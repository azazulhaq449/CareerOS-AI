# syntax=docker/dockerfile:1

# ---- base: shared setup for dev and build stages ----
FROM node:22-alpine AS base
WORKDIR /app

# ---- dev: used by docker-compose.yml (hot reload via bind mount) ----
FROM base AS dev
COPY package.json package-lock.json ./
RUN npm install
EXPOSE 5173
CMD ["npm", "run", "dev"]

# ---- build: compiles the static production bundle ----
FROM base AS build
COPY package.json package-lock.json ./
RUN npm install
COPY . .
# VITE_API_BASE_URL is baked into the JS bundle at build time (Vite doesn't
# read env vars at runtime), so it must arrive as a build ARG here, not as
# a container environment variable later.
ARG VITE_API_BASE_URL
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL
RUN npm run build

# ---- prod: static files served by nginx, no Node runtime needed ----
FROM nginx:alpine AS prod
COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
