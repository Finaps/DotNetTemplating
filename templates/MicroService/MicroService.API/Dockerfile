FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["MicroService.API.csproj", "."]
RUN dotnet restore "MicroService.API.csproj"
COPY . .
RUN dotnet build "MicroService.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicroService.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicroService.API.dll"]