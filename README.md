# InsuranceClaimPortal
To run the project using visual studio
1. Change connection strings to point to your local server
2. In YCompanyThirdPartyAPI - run update-database command to create seed data. 
3. Select web application, identity server and other relevant apis as start up projects
5. Update Microsoft.EntityFrameworkCore.SqlServer to 7.0.0v from Nuget Package Manager in Identity Server and Third Party API

NOTE: Use YCompanyWebApplication instead of YCompanyWebApp as of now as authentication is yet to be configured in the same