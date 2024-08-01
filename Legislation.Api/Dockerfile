FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Legislation.Api/Legislation.Api.csproj", "Legislation.Api/"]
COPY ["Legislation.Data/Legislation.Data.csproj", "Legislation.Data/"]
COPY ["SharedInfrastructure/Infrastructure.csproj", "SharedInfrastructure/"]
RUN dotnet restore "./Legislation.Api/Legislation.Api.csproj"
COPY . .
WORKDIR "/src/Legislation.Api"
RUN dotnet build "./Legislation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Legislation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Legislation.Api.dll"]