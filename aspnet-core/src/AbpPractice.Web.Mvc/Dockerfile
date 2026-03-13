FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/AbpPractice.Web.Mvc/AbpPractice.Web.Mvc.csproj", "src/AbpPractice.Web.Mvc/"]
COPY ["src/AbpPractice.Web.Core/AbpPractice.Web.Core.csproj", "src/AbpPractice.Web.Core/"]
COPY ["src/AbpPractice.Application/AbpPractice.Application.csproj", "src/AbpPractice.Application/"]
COPY ["src/AbpPractice.Core/AbpPractice.Core.csproj", "src/AbpPractice.Core/"]
COPY ["src/AbpPractice.EntityFrameworkCore/AbpPractice.EntityFrameworkCore.csproj", "src/AbpPractice.EntityFrameworkCore/"]
WORKDIR "/src/src/AbpPractice.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/AbpPractice.Web.Mvc", "src/AbpPractice.Web.Mvc"]
COPY ["src/AbpPractice.Web.Core", "src/AbpPractice.Web.Core"]
COPY ["src/AbpPractice.Application", "src/AbpPractice.Application"]
COPY ["src/AbpPractice.Core", "src/AbpPractice.Core"]
COPY ["src/AbpPractice.EntityFrameworkCore", "src/AbpPractice.EntityFrameworkCore"]
WORKDIR "/src/src/AbpPractice.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "AbpPractice.Web.Mvc.dll"]
