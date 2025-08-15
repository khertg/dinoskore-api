# Lightweight runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy everything into /src first
COPY . .

# Restore only the API project
RUN dotnet restore "src/src/DinoSkore.Api/DinoSkore.Api.csproj"

WORKDIR "/src/src/DinoSkore.Api"
RUN dotnet build "DinoSkore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DinoSkore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DinoSkore.Api.dll"]

# # Copy csproj and restore
# COPY ["src/DinoSkore.Api/DinoSkore.Api.csproj", "DinoSkore.Api/"]
# RUN dotnet restore "./src/DinoSkore.Api/DinoSkore.Api.csproj"

# # Copy all and publish
# COPY . .
# WORKDIR "/src/DinoSkore.Api"
# RUN dotnet publish "DinoSkore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# # Final image
# FROM base AS final
# WORKDIR /app
# COPY --from=build /app/publish .
# ENTRYPOINT ["dotnet", "DinoSkore.Api.dll"]

