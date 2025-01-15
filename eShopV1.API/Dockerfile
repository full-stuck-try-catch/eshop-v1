# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["eShopV1.API/eShopV1.API.csproj", "eShopV1.API/"]
COPY ["eShopV1.Application/eShopV1.Application.csproj", "eShopV1.Application/"]
COPY ["eShopv1.Domain/eShopv1.Domain.csproj", "eShopv1.Domain/"]
COPY ["eShopV1.Infrastructure/eShopV1.Infrastructure.csproj", "eShopV1.Infrastructure/"]
RUN dotnet restore "./eShopV1.API/eShopV1.API.csproj"
COPY . .
WORKDIR "/src/eShopV1.API"
RUN dotnet build "./eShopV1.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./eShopV1.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShopV1.API.dll"]