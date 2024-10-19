# Stage 1: Base Image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app
USER $APP_UID

# Stage 2: Build Image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# COPY with correct relative paths
COPY ./KoggoInvestments.Worker.csproj KoggoInvestments.Worker/
COPY ./KoggoInvestments.Persistence.csproj KoggoInvestments.Persistence/
COPY ./KoggoInvestments.Application.csproj KoggoInvestments.Application/
COPY ./KoggoInvestments.Domain.csproj KoggoInvestments.Domain/
COPY ./KoggoInvestments.ServiceDefaults.csproj KoggoInvestments.ServiceDefaults/
RUN dotnet restore "KoggoInvestments.Worker/KoggoInvestments.Worker.csproj"

# Copy the rest of the files and build the app
COPY . .
WORKDIR "/src/KoggoInvestments.Worker"
RUN dotnet build "KoggoInvestments.Worker.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "KoggoInvestments.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KoggoInvestments.Worker.dll"]
