# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/RAID2D.Server/RAID2D.Server.csproj", "Src/RAID2D.Server/"]
COPY ["Src/RAID2D.Shared/RAID2D.Shared.csproj", "Src/RAID2D.Shared/"]
RUN dotnet restore "./Src/RAID2D.Server/RAID2D.Server.csproj"
COPY . .
WORKDIR "/src/Src/RAID2D.Server"
RUN dotnet build "./RAID2D.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RAID2D.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RAID2D.Server.dll"]
