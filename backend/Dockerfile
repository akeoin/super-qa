# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore
COPY *.sln ./
COPY src/AkeoIN.SuperQA.Web.Host/*.csproj ./src/AkeoIN.SuperQA.Web.Host/
COPY src/AkeoIN.SuperQA.Application/*.csproj ./src/AkeoIN.SuperQA.Application/
COPY src/AkeoIN.SuperQA.Core/*.csproj ./src/AkeoIN.SuperQA.Core/
COPY src/AkeoIN.SuperQA.EntityFrameworkCore/*.csproj ./src/AkeoIN.SuperQA.EntityFrameworkCore/
COPY src/AkeoIN.SuperQA.Web.Core/*.csproj ./src/AkeoIN.SuperQA.Web.Core/
COPY src/AkeoIN.SuperQA.Migrator/*.csproj ./src/AkeoIN.SuperQA.Migrator/
COPY test/AkeoIN.SuperQA.Tests/*.csproj ./test/AkeoIN.SuperQA.Tests/
COPY test/AkeoIN.SuperQA.Web.Tests/*.csproj ./test/AkeoIN.SuperQA.Web.Tests/

# Restore dependencies
RUN dotnet restore

# Copy full source
COPY . .

# Build and publish Web.Host
WORKDIR /src/src/AkeoIN.SuperQA.Web.Host
RUN dotnet publish -c Release -o /app/publish/web

# Build and publish Migrator
WORKDIR /src/src/AkeoIN.SuperQA.Migrator
RUN dotnet publish -c Release -o /app/publish/migrator

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000

# Copy published outputs
COPY --from=build /app/publish/web .
COPY --from=build /app/publish/migrator .

# Copy appsettings.json and update JWT configuration
COPY --from=build /src/src/AkeoIN.SuperQA.Web.Host/appsettings.json .
RUN sed -i 's/"IsEnabled": "true"/"IsEnabled": true/g' appsettings.json

EXPOSE 5000 5001

# Create entrypoint script
RUN echo '#!/bin/bash\n\
echo "Y" | dotnet AkeoIN.SuperQA.Migrator.dll\n\
if [ $? -eq 0 ]; then\n\
    dotnet AkeoIN.SuperQA.Web.Host.dll\n\
else\n\
    echo "Migration failed!"\n\
    exit 1\n\
fi' > /app/entrypoint.sh && \
chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
