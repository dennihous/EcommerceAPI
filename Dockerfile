# Use the official .NET image from Microsoft
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["EcommerceAPI.csproj", "./"]
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet build "EcommerceAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "EcommerceAPI.csproj" -c Release -o /app/publish

# Final stage: copy the published application to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceAPI.dll"]