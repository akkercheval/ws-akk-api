# Stage 1: Build & publish (.NET 9)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Restore first for better layer caching (project-only build, no .sln)
COPY AkkWebService.csproj .
RUN dotnet restore AkkWebService.csproj

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish AkkWebService.csproj -c Release -o /app/publish --no-restore

# Stage 2: Runtime (ASP.NET Core 9)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Listen on 8080 inside the container (matches the Service/Ingress).
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AkkWebService.dll"]
