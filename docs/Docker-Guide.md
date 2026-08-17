# Docker Guide — CareerOS

This is a step-by-step walkthrough for running the whole CareerOS stack
(API + both React apps + Postgres) in Docker. It assumes you've never used
Docker before, and explains each concept the first time it comes up.

You don't *have* to use Docker for day-to-day coding — the VS Code tasks
(`server: watch`, `client: dev`, `admin: dev`, `Run: Everything`) still work
exactly as before and are the fastest loop while you're actively editing
code. Docker is an additional way to run everything: useful for proving
"this actually works outside my editor," for onboarding a new machine, and
later for deployment. See [Project-Overview.md](Project-Overview.md) for
what each app does and why the architecture looks the way it does.

There are **two** ways to run the stack:

- **Dev mode** (`docker/docker-compose.yml`) — hot reload, source code is
  mounted straight from your machine into the containers. Use this day to
  day.
- **Production-style mode** (`docker/docker-compose.prod.yml`) — optimized,
  compiled builds, no hot reload. This is closer to how the app would
  actually run once deployed somewhere.

We'll do dev mode first, then production-style, then a troubleshooting
section for the errors you're most likely to hit.

## 0. One-time setup: your `.env` file

Docker Compose needs real secrets (database password, JWT signing key,
admin login) that must never be committed to git. They live in
`docker/.env`, which is gitignored.

If `docker/.env` doesn't already exist, copy the template and fill in real
values:

```
cp docker/.env.example docker/.env
```

Then edit `docker/.env` and replace every `changeme` with a real value. For
`JWT_KEY`, generate a long random string:

```
python -c "import secrets; print(secrets.token_urlsafe(48))"
```

`ADMIN_EMAIL` / `ADMIN_PASSWORD` are the login for the admin panel — the API
creates this account automatically on first startup if it doesn't exist
yet (same seeding logic used outside Docker).

## 1. Confirm Docker is installed and running

Docker Desktop needs to be installed and running in the background (look
for the whale icon in your system tray). Confirm both pieces are available
from a terminal:

```
docker --version
docker compose version
```

Both should print a version number. If either command isn't found, install
Docker Desktop first — everything else in this guide assumes it's already
there.

## 2. What a Dockerfile is

A **Dockerfile** is a recipe: a plain text file that says "start from this
base image, then run these commands" to produce a container image — a
self-contained snapshot of a filesystem plus a command to run. Let's read
`src/server/Dockerfile` top to bottom, since it shows every concept you'll
need:

```dockerfile
# ---- base: shared setup for both dev and build stages ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base
WORKDIR /app

# ---- dev: used by docker-compose.yml (hot reload via bind mount) ----
FROM base AS dev
COPY CareerOS.Server.csproj ./
RUN dotnet restore
COPY . .
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
EXPOSE 5257
CMD ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:5257"]

# ---- build: compiles a release build for the prod stage ----
FROM base AS build
COPY CareerOS.Server.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /publish

# ---- prod: used by docker-compose.prod.yml (small runtime image, no SDK) ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS prod
WORKDIR /app
COPY --from=build /publish .
EXPOSE 5257
ENV ASPNETCORE_URLS=http://0.0.0.0:5257
ENTRYPOINT ["dotnet", "CareerOS.Server.dll"]
```

Line by line, in concepts:

- **`FROM <image> AS <name>`** starts a new stage from a base image, and
  names it so later stages can refer to it. This one Dockerfile actually
  defines *four* named stages (`base`, `dev`, `build`, `prod`) — this is
  called a **multi-stage build**. Docker only builds the stage you ask for
  (see `--target` below), so unrelated stages cost nothing.
- **`mcr.microsoft.com/dotnet/sdk:10.0`** is the full .NET SDK — big, but
  has everything needed to compile code. `mcr.microsoft.com/dotnet/aspnet:10.0`
  (used by `prod`) is just the small *runtime*, with no compiler — you
  can't build with it, only run an already-published app. Using the small
  one for `prod` keeps the final image lean.
- **`WORKDIR /app`** sets the working directory inside the container — like
  `cd /app`, but it also creates the folder if missing.
- **`COPY <host-path> <container-path>`** copies files from your machine
  into the image at build time.
- **`RUN <command>`** executes a command *while building* the image (e.g.
  `dotnet restore` downloads NuGet packages once, baked into the image).
- **`ENV`** sets an environment variable inside the container.
- **`EXPOSE <port>`** documents which port the container listens on (it's
  informational — the actual host↔container port mapping happens in
  Compose, see below).
- **`CMD`** / **`ENTRYPOINT`** is the command that runs when a container
  *starts* (as opposed to `RUN`, which only runs during the build).

Notice `dev` and `build` both `COPY` the `.csproj` file and run
`dotnet restore` *before* copying the rest of the source
(`COPY . .`). This is deliberate: Docker caches each layer, and only
re-runs a step if its inputs changed. Since `.csproj` (your dependency
list) changes far less often than your actual code, this ordering means
`dotnet restore` — the slow step — is skipped on almost every rebuild,
because Docker sees the `.csproj` layer is unchanged and reuses the
cached result.

`src/client/Dockerfile` and `src/admin/Dockerfile` follow the identical
shape, but for a Node/React app instead of .NET:

```dockerfile
FROM node:22-alpine AS base
WORKDIR /app

FROM base AS dev
COPY package.json package-lock.json ./
RUN npm install
EXPOSE 5173
CMD ["npm", "run", "dev"]

FROM base AS build
COPY package.json package-lock.json ./
RUN npm install
COPY . .
ARG VITE_API_BASE_URL
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL
RUN npm run build

FROM nginx:alpine AS prod
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
```

Two things worth calling out here, because they trip people up:

- **`ARG` vs `ENV`**: `ARG VITE_API_BASE_URL` declares a *build-time*
  variable — a value passed in only while the image is being built (via
  `--build-arg` or Compose's `build.args`). Vite reads
  `VITE_API_BASE_URL` and bakes it directly into the compiled JavaScript
  during `npm run build`, so it has to exist *at build time*. By the time
  the `prod` container is actually running, there's no Node process left
  to read a runtime environment variable — nginx is just serving static
  files — so setting `VITE_API_BASE_URL` as a normal `environment:` entry
  in Compose (like the dev stack does) would silently do nothing here.
- **`COPY --from=build`**: this copies files *from another stage's
  filesystem*, not from your machine. The `prod` stage never installs
  Node or runs `npm` at all — it just grabs the already-compiled `dist/`
  folder that the `build` stage produced, and hands it to nginx. That's
  why the final `prod` image is tiny: no Node runtime, no `node_modules`,
  just static HTML/CSS/JS plus nginx.

## 3. What Docker Compose adds

`docker build` and `docker run` work one container at a time. This project
needs four running together (Postgres, API, client, admin) that can talk to
each other by name. **Docker Compose** describes all of them in one YAML
file and starts/stops/rebuilds them as a group with one command.

`docker/docker-compose.yml` (dev mode) has a `services:` entry per
container. Skimming the `server` service:

```yaml
server:
  container_name: careeros-server-dev
  build:
    context: ../src/server
    target: dev
  ports:
    - "5257:5257"
  volumes:
    - ../src/server:/app
    - /app/bin
    - /app/obj
  environment:
    ConnectionStrings__CareerOS: "Host=postgres;Port=5432;..."
  depends_on:
    postgres:
      condition: service_healthy
```

- **`build.target: dev`** tells Compose which named stage from the
  Dockerfile to build — this is the `--target` flag mentioned above,
  applied automatically.
- **`ports: "5257:5257"`** maps `host_port:container_port`. The number on
  the left is what you type into your browser; the number on the right is
  what the app inside the container is listening on. Every service in this
  project uses the same number on both sides, so nothing about the URLs
  you already use changes.
- **`volumes: ../src/server:/app`** is a **bind mount**: it makes the
  container see your actual `src/server` folder on disk, live, instead of
  the copy that was baked in at build time. Edit a file on your machine,
  and the container sees the change instantly — this is what makes hot
  reload possible. `/app/bin` and `/app/obj` are listed separately as
  **anonymous volumes** — explained in the box below, because it's the
  single most confusing part of this setup for a Docker beginner.
- **`depends_on: postgres: condition: service_healthy`** makes the
  `server` container wait until Postgres has actually passed its
  `healthcheck` (see the `postgres` service, which runs `pg_isready` every
  few seconds) before starting. A plain `depends_on` without a condition
  only waits for the Postgres *container to start* — not for the database
  inside it to be ready to accept connections — which would make the API
  crash on its very first migration attempt.

> **The anonymous-volume gotcha.** Bind-mounting `../src/server:/app` puts
> your host's `src/server` folder — which has no `bin`/`obj` yet, or stale
> ones from a local `dotnet build` — directly over whatever the container
> just built inside `/app`. Without anything else, that would hide the
> container's own compiled output the moment it starts. Adding `/app/bin`
> and `/app/obj` as their own volume entries tells Docker "keep these two
> subfolders as container-managed storage instead of pulling them from the
> host mount" — so the container's build output survives underneath the
> bind mount. The client and admin apps have the exact same issue with
> `node_modules`, solved the same way (`/app/node_modules` as its own
> volume entry).

This project uses **two separate Compose files** rather than one file with
profiles — `docker-compose.yml` for dev, `docker-compose.prod.yml` for
production-style — so which one you're running is always explicit from the
command you type, with no flags to remember.

## 4. Build the dev stack

From the repo root:

```
docker compose -f docker/docker-compose.yml build
```

This builds all three custom images (`server`, `client`, `admin`) — the
`dev` target of each Dockerfile — without starting anything yet. The first
build downloads base images (.NET SDK, Node) and restores all packages, so
it can take a few minutes. Run it again and it'll be much faster: Docker
reuses cached layers for anything that hasn't changed (the same caching
behavior explained in step 2).

## 5. Start it

```
docker compose -f docker/docker-compose.yml up -d
```

`-d` runs it in the background ("detached") so you get your terminal back.
Drop `-d` if you want to watch all four containers' logs stream live in
your terminal instead.

Check everyone's status:

```
docker compose -f docker/docker-compose.yml ps
```

You should see all four containers (`postgres`, `server`, `client`,
`admin`) with a status of `Up`, and `postgres` specifically showing
`(healthy)`.

Peek at the API's logs to confirm it actually connected to the database and
applied migrations:

```
docker compose -f docker/docker-compose.yml logs server
```

Look for lines like `No migrations were applied. The database is already
up to date.` (or, on a brand-new database, the migrations actually being
applied) followed by `Now listening on: http://0.0.0.0:5257` and
`Application started.`.

## 6. Confirm it in a browser

Same URLs as always, whether or not Docker is involved:

- API: [http://localhost:5257/api/profile](http://localhost:5257/api/profile)
- Public site: [http://localhost:5173](http://localhost:5173)
- Admin panel: [http://localhost:5174](http://localhost:5174)

Log into the admin panel with the `ADMIN_EMAIL` / `ADMIN_PASSWORD` from
your `docker/.env`.

## 7. Edit something and watch it hot-reload

With the dev stack still running, open `src/client/src/App.tsx` (or any
page under `src/admin`) and make a small, visible change — then check your
browser tab; it should update on its own within a second or two, no
restart needed. This proves the bind mount from step 3 is actually
wired up correctly.

> **If it doesn't reload:** on Windows and macOS, file-change notifications
> from a bind-mounted host folder don't always reach the Linux filesystem
> watcher inside the container — the same reason the server's Dockerfile
> sets `DOTNET_USE_POLLING_FILE_WATCHER=true`. The client and admin
> `vite.config.ts` files handle this the same way: they enable Vite's
> `usePolling` file watcher, but only when the `VITE_DOCKER` environment
> variable is set (which Compose sets automatically for the dev
> containers) — polling works everywhere but uses more CPU, so it's kept
> off for the normal, non-Docker `npm run dev` workflow.

## 8. Stop it

```
docker compose -f docker/docker-compose.yml down
```

This stops and **removes** the four containers and the network Compose
created for them — but your Postgres data is untouched. That's because the
data lives in a **named volume** (`careeros-postgres-data`, declared at the
bottom of the compose file), which is separate storage that `down` doesn't
touch. Run `up -d` again later and your data — profile, experience
entries, the admin account — is exactly as you left it.

(There's a way to delete that data too — covered deliberately last, in the
troubleshooting section, since it's easy to trigger by accident.)

## 9. Production-style mode

```
docker compose -f docker/docker-compose.prod.yml up -d --build
```

This builds and starts the same four services, but from each Dockerfile's
`prod` stage instead of `dev`. What's actually different:

| | Dev mode | Production-style mode |
|---|---|---|
| React apps served by | Vite's dev server | nginx, serving a compiled build |
| Source code | Bind-mounted (live-edit) | Baked into the image at build time |
| Edit-and-see-it workflow | Yes, hot reload | No — requires a rebuild |
| `server` runs via | `dotnet watch run` (SDK image) | `dotnet CareerOS.Server.dll` (small runtime image) |
| `ASPNETCORE_ENVIRONMENT` | `Development` | `Production` |

You can confirm you're actually looking at the nginx-served build (not
Vite) by checking the response headers — `curl -I http://localhost:5173`
should show `Server: nginx`, not Vite's dev output.

Everything else — the URLs, the admin login, the database — behaves the
same as dev mode, and the same `down` / data-persistence rules from step 8
apply. Bring it down with:

```
docker compose -f docker/docker-compose.prod.yml down
```

## 10. Troubleshooting

**`port is already allocated` / "only one usage of each socket address"**
Something else on your machine is already listening on that port — most
often a leftover `dotnet watch` or `npm run dev` process from running the
VS Code tasks earlier and not stopping them. Find and stop it:

```
# find what's using, say, port 5173
docker compose -f docker/docker-compose.yml ps        # is it actually another compose stack?
```

On Windows, if it's not Docker, find the process holding the port and stop
it (PowerShell): `Get-NetTCPConnection -LocalPort 5173 -State Listen`
gives you the `OwningProcess` (PID), then `Stop-Process -Id <pid> -Force`.
Only two compose stacks (dev and prod) can't run at once anyway, since they
use the same host ports on purpose — bring one down before starting the
other.

**"I changed something but the container isn't picking it up"**
If you edited *source code* while the dev stack is running, it should
hot-reload automatically (step 7) — if it doesn't, see the polling note
there. If you added a new **dependency** (a NuGet package or an npm
package), a running container won't see it: dependencies are installed
during the image *build*, not at runtime. Rebuild:

```
docker compose -f docker/docker-compose.yml up -d --build
```

**The `-v` data-loss trap**
`docker compose down` on its own is safe — it never touches named volumes.
But `docker compose down -v` additionally deletes every volume the compose
file declares, which for this project means **your entire Postgres
database** (profile, experience, projects, the admin account — everything)
is gone permanently. Only reach for `-v` when you deliberately want a
completely clean slate (e.g. testing migrations from zero). If you're ever
unsure, run a plain `down` — you can always add `-v` in a follow-up command
if you actually meant to wipe it, but you can't undo it after the fact.

## Glossary

- **Image** — a built, immutable snapshot of a filesystem plus a startup
  command, produced by `docker build` from a Dockerfile. Doesn't run by
  itself.
- **Container** — a running instance of an image. You can start multiple
  containers from the same image.
- **Dockerfile** — the recipe used to build an image.
- **Multi-stage build** — one Dockerfile with several named `FROM ... AS
  <name>` stages; `--target` picks which one to actually build.
- **Docker Compose** — a tool (and YAML file format) for defining and
  running multiple containers together as one unit.
- **Bind mount** — a live link from a folder on your machine into a
  container, so file edits show up inside the container instantly.
- **Named volume** — separate, Docker-managed storage (not tied to a
  specific host folder) that survives `docker compose down`. Used here for
  the Postgres data directory.
- **Anonymous volume** — a volume with no name, declared inline (e.g.
  `/app/node_modules`), used here specifically to stop a bind mount from
  hiding a container-built folder underneath it.
- **Healthcheck** — a command Docker runs periodically inside a container
  to decide if it's actually ready (not just started) — used here so the
  API waits for Postgres to accept connections, not just for its
  container to exist.
