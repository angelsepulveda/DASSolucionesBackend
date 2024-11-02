FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DaSSolucionesBackend.Api/DaSSolucionesBackend.Api.csproj", "DaSSolucionesBackend.Api/"]
COPY ["DASSolucionesBackend.General/DASSolucionesBackend.General.csproj", "DASSolucionesBackend.General/"]
COPY ["DASSolucionesBackend.Shared/DASSolucionesBackend.Shared.csproj", "DASSolucionesBackend.Shared/"]
COPY ["DASSolucionesBackend.Sales/DASSolucionesBackend.Sales.csproj", "DASSolucionesBackend.Sales/"]
COPY ["DASSolucionesBackend.Security/DASSolucionesBackend.Security.csproj", "DASSolucionesBackend.Security/"]
COPY ["DASSolucionesBackend.Shopping/DASSolucionesBackend.Shopping.csproj", "DASSolucionesBackend.Shopping/"]
COPY ["DASSolucionesBackend.Warehouses/DASSolucionesBackend.Warehouses.csproj", "DASSolucionesBackend.Warehouses/"]
RUN dotnet restore "DaSSolucionesBackend.Api/DaSSolucionesBackend.Api.csproj"
COPY . .
WORKDIR "/src/DaSSolucionesBackend.Api"
RUN dotnet build "DaSSolucionesBackend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DaSSolucionesBackend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DaSSolucionesBackend.Api.dll"]
