FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KoggoInvestments.Worker/KoggoInvestments.Worker.csproj", "KoggoInvestments.Worker/"]
COPY ["KoggoInvestments.Persistence/KoggoInvestments.Persistence.csproj", "KoggoInvestments.Persistence/"]
COPY ["KoggoInvestments.Application/KoggoInvestments.Application.csproj", "KoggoInvestments.Application/"]
COPY ["KoggoInvestments.Domain/KoggoInvestments.Domain.csproj", "KoggoInvestments.Domain/"]
COPY ["KoggoInvestments.ServiceDefaults/KoggoInvestments.ServiceDefaults.csproj", "KoggoInvestments.ServiceDefaults/"]
RUN dotnet restore "KoggoInvestments.Worker/KoggoInvestments.Worker.csproj"
COPY . .
WORKDIR "/src/KoggoInvestments.Worker"
RUN dotnet build "KoggoInvestments.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KoggoInvestments.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KoggoInvestments.Worker.dll"]
