# Establece la imagen base para la ejecución de la aplicación (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Establece la imagen base para la construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["ECU.API.AUTH/ECU.API.AUTH.csproj", "ECU.API.AUTH/"]
RUN dotnet restore "ECU.API.AUTH/ECU.API.AUTH.csproj"
COPY . .
WORKDIR "/src/ECU.API.AUTH"
RUN dotnet build "ECU.API.AUTH.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "ECU.API.AUTH.csproj" -c Release -o /app/publish

# Imagen final para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECU.API.AUTH.dll"]
