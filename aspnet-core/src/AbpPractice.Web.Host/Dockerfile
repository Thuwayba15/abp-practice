FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/AbpPractice.Web.Host/AbpPractice.Web.Host.csproj", "src/AbpPractice.Web.Host/"]
COPY ["src/AbpPractice.Web.Core/AbpPractice.Web.Core.csproj", "src/AbpPractice.Web.Core/"]
COPY ["src/AbpPractice.Application/AbpPractice.Application.csproj", "src/AbpPractice.Application/"]
COPY ["src/AbpPractice.Core/AbpPractice.Core.csproj", "src/AbpPractice.Core/"]
COPY ["src/AbpPractice.EntityFrameworkCore/AbpPractice.EntityFrameworkCore.csproj", "src/AbpPractice.EntityFrameworkCore/"]
WORKDIR "/src/src/AbpPractice.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/AbpPractice.Web.Host", "src/AbpPractice.Web.Host"]
COPY ["src/AbpPractice.Web.Core", "src/AbpPractice.Web.Core"]
COPY ["src/AbpPractice.Application", "src/AbpPractice.Application"]
COPY ["src/AbpPractice.Core", "src/AbpPractice.Core"]
COPY ["src/AbpPractice.EntityFrameworkCore", "src/AbpPractice.EntityFrameworkCore"]
WORKDIR "/src/src/AbpPractice.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AbpPractice.Web.Host.dll"]
