FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /
RUN mkdir -p /https-root
COPY ["Gateway/aspnetapp-root-cert.cer", "/https-root/aspnetapp-root-cert.cer"]
COPY ["Gateway/docker-entrypoint.sh", "/docker-entrypoint.sh"]
RUN chmod +x docker-entrypoint.sh
RUN ./docker-entrypoint.sh
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gateway/Gateway.csproj", "Gateway/"]
RUN dotnet restore "Gateway/Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway"
RUN dotnet build "Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.dll"]
