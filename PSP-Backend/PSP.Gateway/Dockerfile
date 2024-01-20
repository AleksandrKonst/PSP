FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PSP_Gateway/PSP_Gateway.csproj", "PSP_Gateway/"]
RUN dotnet restore "PSP_Gateway/PSP_Gateway.csproj"
COPY . .
WORKDIR "/src/PSP_Gateway"
RUN dotnet build "PSP_Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PSP_Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PSP_Gateway.dll"]
