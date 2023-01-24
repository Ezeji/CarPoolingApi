FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 9091

ENV ASPNETCORE_URLS=http://+:9091

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY CarPoolingService/CarPoolingApi/CarPoolingApi.csproj CarPoolingApi/
COPY CarPoolingService/CarPoolingCore/CarPoolingCore.csproj CarPoolingCore/
COPY CarPoolingService/CarPoolingInfrastructure/CarPoolingInfrastructure.csproj CarPoolingInfrastructure/
RUN dotnet restore "CarPoolingApi/CarPoolingApi.csproj"
COPY . .
WORKDIR "/src/CarPoolingService/CarPoolingApi"
RUN dotnet build "CarPoolingApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarPoolingApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarPoolingApi.dll"]

