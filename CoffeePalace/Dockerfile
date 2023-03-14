FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CoffeePalace/CoffeePalace.csproj", "CoffeePalace/"]
RUN dotnet restore "CoffeePalace/CoffeePalace.csproj"
COPY . .
WORKDIR "/src/CoffeePalace"
RUN dotnet build "CoffeePalace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoffeePalace.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeePalace.dll"]
