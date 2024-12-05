#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ThirdPartyAndSeed/YCompanyThirdPartyAPI/YCompanyThirdPartyAPI.csproj", "ThirdPartyAndSeed/YCompanyThirdPartyAPI/"]
COPY ["Services/YCompanyIdentityServer/YCompanyIdentityServer/YCompanyIdentityServer.csproj", "Services/YCompanyIdentityServer/YCompanyIdentityServer/"]
COPY ["Framework/YCompany.Claims.Caching/YCompany.Caching.csproj", "Framework/YCompany.Claims.Caching/"]
COPY ["Framework/YCompany.Claims.ExceptionHandling/YCompany.ExceptionHandling.csproj", "Framework/YCompany.Claims.ExceptionHandling/"]
COPY ["Framework/YCompany.Claims.Logging/YCompany.Logging.csproj", "Framework/YCompany.Claims.Logging/"]
COPY ["Framework/YCompany.Claims.MessagingQueue/YCompany.MessagingQueue.csproj", "Framework/YCompany.Claims.MessagingQueue/"]
COPY ["Framework/YCompany.HealthChecks/YCompany.HealthChecks.csproj", "Framework/YCompany.HealthChecks/"]
COPY ["Services/YCompanyPaymentsAPI/YCompanyPaymentsAPI/YCompanyPaymentsAPI.csproj", "Services/YCompanyPaymentsAPI/YCompanyPaymentsAPI/"]
COPY ["Services/YCompanyPaymentsAPI/YCompany.Payments.Domain/YCompany.Payments.Domain.csproj", "Services/YCompanyPaymentsAPI/YCompany.Payments.Domain/"]
COPY ["Services/YCompanyPaymentsAPI/YCompany.Payments.Services/YCompany.Payments.Services.csproj", "Services/YCompanyPaymentsAPI/YCompany.Payments.Services/"]
COPY ["YCompany.Configurations/YCompany.Configurations.csproj", "YCompany.Configurations/"]
RUN dotnet restore "./ThirdPartyAndSeed/YCompanyThirdPartyAPI/YCompanyThirdPartyAPI.csproj"
COPY . .
WORKDIR "/src/ThirdPartyAndSeed/YCompanyThirdPartyAPI"
RUN dotnet build "./YCompanyThirdPartyAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YCompanyThirdPartyAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YCompanyThirdPartyAPI.dll"]