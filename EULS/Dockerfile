# Use the official .NET runtime image
FROM mcr.microsoft.com/dotnet/asp:7.0 AS base
WORKDIR /app

# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o app

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "EULS.dll"]