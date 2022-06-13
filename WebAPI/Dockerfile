FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders
COPY ["./Application/Application.csproj", "src/ContainerNinja.Contracts/"]
COPY ["./Infrastructure/Infrastructure.csproj", "src/ContainerNinja.Migrations/"]
COPY ["./Persistence/Persistence.csproj", "src/ContainerNinja.Infrastructure/"]
COPY ["./Domain/Domain.csproj", "src/ContainerNinja.Core/"]
COPY ["./WebAPI/WebAPI.csproj", "src/ContainerNinja.API/"]