#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt install -y nodejs
WORKDIR /src
COPY ["src/WebUI/WebUI.csproj", "src/WebUI/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS tests
WORKDIR /src/tests/Application.UnitTests
RUN dotnet test

FROM tests AS publish
WORKDIR /src/src/WebUI
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]