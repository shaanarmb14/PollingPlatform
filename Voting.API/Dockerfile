#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5004
EXPOSE 5005

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Voting.Api/Voting.Api.csproj", "Voting.Api/"]
COPY ["SharedInfrastructure/Infrastructure.csproj", "SharedInfrastructure/"]
RUN dotnet restore "./Voting.Api/Voting.Api.csproj"
COPY . .
WORKDIR "/src/Voting.Api"
RUN dotnet build "./Voting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Voting.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Voting.Api.dll"]