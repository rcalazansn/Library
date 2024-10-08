#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Library.API/Library.API.csproj", "src/Library.API/"]
COPY ["src/Library.Application/Library.Application.csproj", "src/Library.Application/"]
COPY ["src/Library.Core/Library.Core.csproj", "src/Library.Core/"]
COPY ["src/Library.Domain/Library.Domain.csproj", "src/Library.Domain/"]
COPY ["src/Library.Infrastructure/Library.Infrastructure.csproj", "src/Library.Infrastructure/"]
RUN dotnet restore "./src/Library.API/Library.API.csproj"
COPY . .
WORKDIR "/src/src/Library.API"
RUN dotnet build "./Library.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Library.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
#80 (default 8080)
ENV ASPNETCORE_URLS="http://+:80"
#SWAGGER
ENV ASPNETCORE_ENVIRONMENT="Development" 

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Library.API.dll"]