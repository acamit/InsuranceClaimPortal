#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/YCompanyVendorAPI/YCompanyVendorAPI/YCompanyVendorAPI.csproj", "Services/YCompanyVendorAPI/YCompanyVendorAPI/"]
COPY ["Services/YCompanyClaimsAPI/YCompany.Claims.DataAccess/YCompany.Claims.DataAccess.csproj", "Services/YCompanyClaimsAPI/YCompany.Claims.DataAccess/"]
COPY ["Services/YCompanyClaimsAPI/YCompany.Claims.Domain/YCompany.Claims.Domain.csproj", "Services/YCompanyClaimsAPI/YCompany.Claims.Domain/"]
COPY ["Framework/YCompany.HealthChecks/YCompany.HealthChecks.csproj", "Framework/YCompany.HealthChecks/"]
COPY ["Framework/YCompany.Claims.MessagingQueue/YCompany.MessagingQueue.csproj", "Framework/YCompany.Claims.MessagingQueue/"]
COPY ["Services/YCompanyVendorAPI/YCompany.Vendor.DataAccess/YCompany.Vendor.DataAccess.csproj", "Services/YCompanyVendorAPI/YCompany.Vendor.DataAccess/"]
COPY ["Services/YCompanyVendorAPI/YCompany.Vendor.Domain/YCompany.Vendor.Domain.csproj", "Services/YCompanyVendorAPI/YCompany.Vendor.Domain/"]
RUN dotnet restore "./Services/YCompanyVendorAPI/YCompanyVendorAPI/YCompanyVendorAPI.csproj"
COPY . .
WORKDIR "/src/Services/YCompanyVendorAPI/YCompanyVendorAPI"
RUN dotnet build "./YCompanyVendorAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YCompanyVendorAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YCompanyVendorAPI.dll"]