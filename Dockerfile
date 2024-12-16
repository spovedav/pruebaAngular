# Establece la imagen base para la ejecuci�n de la aplicaci�n (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Establece la imagen base para la construcci�n de la aplicaci�n
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["ECU.API.AUTH/ECU.API.AUTH.csproj", "ECU.API.AUTH/"]
RUN dotnet restore "ECU.API.AUTH/ECU.API.AUTH.csproj"
COPY . .
WORKDIR "/src/ECU.API.AUTH"
RUN dotnet build "ECU.API.AUTH.csproj" -c Release -o /app/build

# Publicar la aplicaci�n
FROM build AS publish
RUN dotnet publish "ECU.API.AUTH.csproj" -c Release -o /app/publish

# Imagen final para ejecutar la aplicaci�n
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECU.API.AUTH.dll"]
