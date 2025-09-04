# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["dotnet-backend/AirlineBookingSystem.sln", "dotnet-backend/"]
COPY ["dotnet-backend/AirlineBookingSystem.API/AirlineBookingSystem.API.csproj", "dotnet-backend/AirlineBookingSystem.API/"]
COPY ["dotnet-backend/AirlineBookingSystem.Application/AirlineBookingSystem.Application.csproj", "dotnet-backend/AirlineBookingSystem.Application/"]
COPY ["dotnet-backend/AirlineBookingSystem.Domain/AirlineBookingSystem.Domain.csproj", "dotnet-backend/AirlineBookingSystem.Domain/"]
COPY ["dotnet-backend/AirlineBookingSystem.Infrastructure/AirlineBookingSystem.Infrastructure.csproj", "dotnet-backend/AirlineBookingSystem.Infrastructure/"]
COPY ["dotnet-backend/AirlineBookingSystem.Persistence/AirlineBookingSystem.Persistence.csproj", "dotnet-backend/AirlineBookingSystem.Persistence/"]
COPY ["dotnet-backend/AirlineBookingSystem.Shared/AirlineBookingSystem.Shared.csproj", "dotnet-backend/AirlineBookingSystem.Shared/"]

# Restore dependencies
RUN dotnet restore "dotnet-backend/AirlineBookingSystem.sln"

# Copy the rest of the source code
COPY ["dotnet-backend/", "dotnet-backend/"]
WORKDIR "/src/dotnet-backend"

# Publish the application
RUN dotnet publish "AirlineBookingSystem.API/AirlineBookingSystem.API.csproj" -c Release -o /app/publish

# Stage 2: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port
EXPOSE 8080

ENTRYPOINT ["dotnet", "AirlineBookingSystem.API.dll"]
