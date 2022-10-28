FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN ls
WORKDIR /src
COPY . .
RUN dotnet restore "RestaurantBackend.sln"
COPY . .
WORKDIR /src
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN ls
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restaurant.App.dll"]
