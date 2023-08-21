
# See https://aka.ms/customizecontainer to learn how to customize your container and how Visual debug Studio uses this Dockerfile to build your images for faster debugging.

# Set the base image and working directory
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Expose the necessary ports
# Remove the second EXPOSE directive since we only need to expose one port
EXPOSE 5289/tcp

# Set the image for building the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NinjaTalentCountrys.csproj", "."]
RUN dotnet restore "./NinjaTalentCountrys.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NinjaTalentCountrys.csproj" -c Release -o /app/build

# Set the image for publishing the application
FROM build AS publish
RUN dotnet publish "NinjaTalentCountrys.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Set the final image and working directory
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NinjaTalentCountrys.dll"]
