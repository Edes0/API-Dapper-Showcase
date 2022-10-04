# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build-env
WORKDIR /app

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore --disable-parallel

# Build and publish a release
RUN dotnet publish -c Release -o out --no-restore

# Server Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
EXPOSE 8080
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Mimbly.Api.dll"]