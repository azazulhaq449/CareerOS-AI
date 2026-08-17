# syntax=docker/dockerfile:1

# ---- base: shared setup for both dev and build stages ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base
WORKDIR /app

# ---- dev: used by docker-compose.yml (hot reload via bind mount) ----
FROM base AS dev
COPY CareerOS.Server.csproj ./
RUN dotnet restore
COPY . .
# Docker's bind-mounted filesystem doesn't always emit the native file-change
# events dotnet watch relies on, especially on Windows — polling is slower
# but reliable everywhere.
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
