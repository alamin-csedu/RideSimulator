# RideSimulator
## Description

This is a .NET Core 6 project that utilizes SQL Server as its database backend. It serves as a template for building web applications, APIs, or any other type of application using this technology stack.

### For docker container
  I giving yml code you just save
  ```
  version: '3'

networks:
  backend:

services:
  ridersimulatordb:
    image: mcr.microsoft.com/mssql/server:2019-latest
    environment:
        ACCEPT_EULA: "Y"
        MSSQL_SA_PASSWORD: "Pathao@1234"
    ports:
      - 1433:1433
    networks:
      backend:
  ridesimulator:
    image: alamin007/ridesimulator
    ports:
      - 8989:80
    environment:
      - DB_HOST=ridersimulatordb
      - DB_NAME=ridesimulator
      - DB_SA_PASSWORD=Pathao@1234
```
Then run the command to pull and run the container in your docker desktop
```
docker-compose up
```
You can open the docker desktop and then visit the app container URL to access the app

### For manual running application
Before running this project, ensure you have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or any other edition)
- [Sql Server Management Studio] (optional if you need to see database)

1. Then you clone the project by command in git bash
    git clone https://github.com/alamin-csedu/RideSimulator.git

2. Navigate to project directory
   ```
   cd your-project
4. Restore dependencies using the .NET CLI:
   ```
    dotnet restore

Configuration
1. Open the appsettings.json file located in the project's root directory.
2. Update the ConnectionStrings section with your SQL Server connection string. Example:

```  
          "DefaultConnection": "Server=LAPTOP-IDKBJF6U;Database=RideSimulator;Trusted_Connection=True;TrustServerCertificate=True"
```
  Just update the server is your computer name for windows authentication in sql server
Again for database setup
For migration
```
  dotnet ef database update
```
For run application
```
dotnet run


  
