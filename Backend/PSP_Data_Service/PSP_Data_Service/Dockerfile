FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PSP_Data_Service/PSP_Data_Service.csproj", "PSP_Data_Service/"]
RUN dotnet restore "PSP_Data_Service/PSP_Data_Service.csproj"
COPY . .
WORKDIR "/src/PSP_Data_Service"
RUN dotnet build "PSP_Data_Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PSP_Data_Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PSP_Data_Service.dll"]
