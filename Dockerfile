#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
#PARA DOCKER
EXPOSE 80
#PARA LOCAL
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NinjaTalentCountrys.csproj", "."]
RUN dotnet restore "./NinjaTalentCountrys.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NinjaTalentCountrys.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NinjaTalentCountrys.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NinjaTalentCountrys.dll"]
