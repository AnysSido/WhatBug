FROM node:24-bookworm-slim AS frontend
WORKDIR /frontend
COPY src/WebUI/package.json src/WebUI/yarn.lock ./
RUN yarn install --frozen-lockfile --non-interactive
COPY src/WebUI/ ./
RUN yarn build
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/WebUI/WebUI.csproj", "src/WebUI/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/WebUI/WebUI.csproj"
COPY . .
COPY --from=frontend /frontend/wwwroot ./src/WebUI/wwwroot
WORKDIR "/src/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS tests
WORKDIR /src/tests/Application.UnitTests
RUN dotnet test -p:SkipFrontendBuild=true

FROM tests AS publish
WORKDIR /src/src/WebUI
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish -p:SkipFrontendBuild=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
