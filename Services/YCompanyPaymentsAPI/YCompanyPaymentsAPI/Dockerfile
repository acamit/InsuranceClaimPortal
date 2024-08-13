#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/YCompanyPaymentsAPI/YCompanyPaymentsAPI/YCompanyPaymentsAPI.csproj", "Services/YCompanyPaymentsAPI/YCompanyPaymentsAPI/"]
COPY ["Framework/YCompany.HealthChecks/YCompany.HealthChecks.csproj", "Framework/YCompany.HealthChecks/"]
COPY ["Framework/YCompany.Claims.MessagingQueue/YCompany.MessagingQueue.csproj", "Framework/YCompany.Claims.MessagingQueue/"]
COPY ["Services/YCompanyPaymentsAPI/YCompany.Payments.Domain/YCompany.Payments.Domain.csproj", "Services/YCompanyPaymentsAPI/YCompany.Payments.Domain/"]
COPY ["Services/YCompanyPaymentsAPI/YCompany.Payments.Services/YCompany.Payments.Services.csproj", "Services/YCompanyPaymentsAPI/YCompany.Payments.Services/"]
RUN dotnet restore "./Services/YCompanyPaymentsAPI/YCompanyPaymentsAPI/YCompanyPaymentsAPI.csproj"
COPY . .
WORKDIR "/src/Services/YCompanyPaymentsAPI/YCompanyPaymentsAPI"
RUN dotnet build "./YCompanyPaymentsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YCompanyPaymentsAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YCompanyPaymentsAPI.dll"]