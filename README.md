Demonstrating a Trading API developed in .NET Core 8 which allows users to retrieve and execute trades. This project also contains a console application which polls for messages on trade creation and logs the created trades in the console.

Steps to run the applications locally:
- Open Command Prompt at the repository root (ex: {path}/trading-demo)
- Please execute the following commands:
```
docker pull postgres:latest
docker run -d --name my-postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=mydb -p 5432:5432 postgres:latest

docker pull rabbitmq:3-management
docker run -d --name my-rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management

dotnet ef migrations add SeedingTestData --startup-project src/Trading.API/Trading.API.csproj  --project src/Trading.Infrastructure.Data/Trading.Infrastructure.Data.csproj
dotnet ef database update --startup-project src/Trading.API/Trading.API.csproj  --project src/Trading.Infrastructure.Data/Trading.Infrastructure.Data.csproj
```
- Open Trading.sln
- Configure Startup Projects to be Trading.API and Trading.Console
- Run

Methodologies used:
- Clean Architecture
- CQRS Pattern

.NET Core Libraries used:
- Entity Framework Core
- Mediatr Library
- FluentValidation Library
- AutoMapper Library
- MassTransit
- Serilog
- Moq
- XUnit

Other Technologies used:
- RabbitMq
- Postgresql  

Future improvements:
- Prepare a dockerized image of the API application
- Prepare a dockerized image of the Console application
- Prepare a dockerized image of the Postgres database to contain a particular snapshot of data
- Wrap the dockerized images of my applications + rabbitmq image + postgres image + postgres snapshot image into a docker-compose.yml file
- Implement authentication to gain information on the currently logged in user
