FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/AkeoIN.SuperQA.Web.Host/AkeoIN.SuperQA.Web.Host.csproj", "src/AkeoIN.SuperQA.Web.Host/"]
COPY ["src/AkeoIN.SuperQA.Web.Core/AkeoIN.SuperQA.Web.Core.csproj", "src/AkeoIN.SuperQA.Web.Core/"]
COPY ["src/AkeoIN.SuperQA.Application/AkeoIN.SuperQA.Application.csproj", "src/AkeoIN.SuperQA.Application/"]
COPY ["src/AkeoIN.SuperQA.Core/AkeoIN.SuperQA.Core.csproj", "src/AkeoIN.SuperQA.Core/"]
COPY ["src/AkeoIN.SuperQA.EntityFrameworkCore/AkeoIN.SuperQA.EntityFrameworkCore.csproj", "src/AkeoIN.SuperQA.EntityFrameworkCore/"]
WORKDIR "/src/src/AkeoIN.SuperQA.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/AkeoIN.SuperQA.Web.Host", "src/AkeoIN.SuperQA.Web.Host"]
COPY ["src/AkeoIN.SuperQA.Web.Core", "src/AkeoIN.SuperQA.Web.Core"]
COPY ["src/AkeoIN.SuperQA.Application", "src/AkeoIN.SuperQA.Application"]
COPY ["src/AkeoIN.SuperQA.Core", "src/AkeoIN.SuperQA.Core"]
COPY ["src/AkeoIN.SuperQA.EntityFrameworkCore", "src/AkeoIN.SuperQA.EntityFrameworkCore"]
WORKDIR "/src/src/AkeoIN.SuperQA.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AkeoIN.SuperQA.Web.Host.dll"]
