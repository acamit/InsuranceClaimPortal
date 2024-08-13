#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/YCompanyIdentityServer/YCompanyIdentityServer/YCompanyIdentityServer.csproj", "Services/YCompanyIdentityServer/YCompanyIdentityServer/"]
COPY ["Framework/YCompany.Claims.Caching/YCompany.Caching.csproj", "Framework/YCompany.Claims.Caching/"]
COPY ["Framework/YCompany.Claims.ExceptionHandling/YCompany.ExceptionHandling.csproj", "Framework/YCompany.Claims.ExceptionHandling/"]
COPY ["Framework/YCompany.Claims.Logging/YCompany.Logging.csproj", "Framework/YCompany.Claims.Logging/"]
COPY ["Framework/YCompany.Claims.MessagingQueue/YCompany.MessagingQueue.csproj", "Framework/YCompany.Claims.MessagingQueue/"]
COPY ["Framework/YCompany.HealthChecks/YCompany.HealthChecks.csproj", "Framework/YCompany.HealthChecks/"]
RUN dotnet restore "./Services/YCompanyIdentityServer/YCompanyIdentityServer/YCompanyIdentityServer.csproj"
COPY . .
WORKDIR "/src/Services/YCompanyIdentityServer/YCompanyIdentityServer"
RUN dotnet build "./YCompanyIdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YCompanyIdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YCompanyIdentityServer.dll"]