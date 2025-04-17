# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY ["backend/AkeoIN.SuperQA.sln", "./"]
COPY ["backend/NuGet.Config", "./"]

# Copy all project files
COPY ["backend/src/AkeoIN.SuperQA.Core/AkeoIN.SuperQA.Core.csproj", "src/AkeoIN.SuperQA.Core/"]
COPY ["backend/src/AkeoIN.SuperQA.Application/AkeoIN.SuperQA.Application.csproj", "src/AkeoIN.SuperQA.Application/"]
COPY ["backend/src/AkeoIN.SuperQA.EntityFrameworkCore/AkeoIN.SuperQA.EntityFrameworkCore.csproj", "src/AkeoIN.SuperQA.EntityFrameworkCore/"]
COPY ["backend/src/AkeoIN.SuperQA.Web.Core/AkeoIN.SuperQA.Web.Core.csproj", "src/AkeoIN.SuperQA.Web.Core/"]
COPY ["backend/src/AkeoIN.SuperQA.Web.Host/AkeoIN.SuperQA.Web.Host.csproj", "src/AkeoIN.SuperQA.Web.Host/"]
COPY ["backend/src/AkeoIN.SuperQA.Migrator/AkeoIN.SuperQA.Migrator.csproj", "src/AkeoIN.SuperQA.Migrator/"]
COPY ["backend/test/AkeoIN.SuperQA.Tests/AkeoIN.SuperQA.Tests.csproj", "test/AkeoIN.SuperQA.Tests/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the code
COPY backend/src/ src/
COPY backend/test/ test/

# Build and publish
RUN dotnet build "AkeoIN.SuperQA.sln" -c Release -o /app/build
RUN dotnet publish "AkeoIN.SuperQA.sln" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "AkeoIN.SuperQA.Web.Host.dll"] 