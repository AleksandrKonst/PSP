FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /
RUN mkdir -p /https-root
COPY ["WebApi/aspnetapp-root-cert.cer", "/https-root/aspnetapp-root-cert.cer"]
COPY ["WebApi/docker-entrypoint.sh", "/docker-entrypoint.sh"]
RUN chmod +x docker-entrypoint.sh
RUN ./docker-entrypoint.sh
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["WebApi/WebApi.csproj", "WebApi/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]