# Stage 1: Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
USER $APP_UID
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build Image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# COPY with correct relative paths
COPY ./KoggoInvestments.Api.csproj KoggoInvestments.Api/
COPY ../KoggoInvestments.Persistence/KoggoInvestments.Persistence.csproj KoggoInvestments.Persistence/
COPY ../KoggoInvestments.Application/KoggoInvestments.Application.csproj KoggoInvestments.Application/
COPY ../KoggoInvestments.Domain/KoggoInvestments.Domain.csproj KoggoInvestments.Domain/
COPY ../KoggoInvestments.ServiceDefaults/KoggoInvestments.ServiceDefaults.csproj KoggoInvestments.ServiceDefaults/
RUN dotnet restore "./KoggoInvestments.Api.csproj"

# Copy the rest of the files and build the app
COPY . .
WORKDIR "/src/KoggoInvestments.Api"
RUN dotnet build "KoggoInvestments.Api.csproj" -c Release -o /app/build

# Stage 3: Publish
FROM build AS publish
RUN dotnet publish "KoggoInvestments.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KoggoInvestments.Api.dll"]
