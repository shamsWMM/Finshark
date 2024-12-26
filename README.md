# ASP.NET Web API .NET 8
This project is built follwing [ASP.NET Web API 8 Tutorial](https://youtube.com/playlist?list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc&si=24d8OpHg2V-pP4kR) by [Teddy Smith](https://www.youtube.com/@TeddySmithDev). The [source code](https://github.com/teddysmithdev/FinShark.git) for the tutorial was used in this project.

## 1. Prerequisites
This project was build on a Mac. The below tools were used.
- .NET 8: https://dotnet.microsoft.com/en-us/download
- VS Code
- Docker (for Azure SQL)
- VS Code Extensions
    - C#
    - C# Dev Kit
    - .NET Extension Pack
    - .NET Install Tool
    - NuGet Gallery
    - Prettier - Code formatter
    - C# Extensions by JosKreativ
## 2. Creating and Launching API
```bash
cd Finshark
# Build a new api project inside a new folder named "api"
dotnet new webapi -o api
cd api
# Start api
donent watch run
```
## 3. Adding NuGet Packgages
- Use **&#x2318;** + **&#x21E7;** + **P** to open the Command Pallete on VS code
- Select `NuGet: Focus on NuGet View` provided by the NuGet Gallery extension, and look up and install:
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.EntityFrameworkCore.Design
## 4. Connecting to SQL Database and Setting Up Migrations
- Install and run [Azure SQL Edge](https://hub.docker.com/r/microsoft/azure-sql-edge)
```bash
docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=Admin123$' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
```
- Install [Azure Data Studio ](https://azure.microsoft.com/en-us/products/data-studio) and create a new Microsoft SQL Server connection with the connection string `"Data Source=localhost;User Id=sa;Password=Admin123$;Integrated Security=True;TrustServerCertificate=true;Trusted_Connection=false"`
- Build database
```bash
dotnet tool install --global dotnet-ef --version 8.0.0
dotnet ef migrations add init
dotnet ef database update
```