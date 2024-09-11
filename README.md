### Library - built with ASP.NET 8 

---

#### Getting Started

###### DotNet
```sh
1. dotnet dev-certs https --clean
2. dotnet dev-certs https --trust
```

###### EF Core
```sh
1. dotnet tool install --global dotnet-ef
```

###### Sql Server Docker
```sh
1. docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server
2. Server=localhost,1433;Database=Library;User ID=sa;Password=1q2w3e4r@#$
2.1. Server=localhost,1433;Database=Library;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;
```

###### RabbitMQ
```sh
1. docker run -d --hostname rabbit-local --name library-rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=library -e RABBITMQ_DEFAULT_PASS=library!@# rabbitmq:3-management
2. http://localhost:15672
```

###### K6
```sh
1. [install-k6](https://grafana.com/docs/k6/latest/set-up/install-k6/) 
2. .\Library\test\k6
3. run the command: k6 run --vus 10 --duration 10s script.js
```

---
#### Run
###### Locally
```sh
- Visual Studio / VS Code
- .Net Core 8
- Docker
- SQLServer
- RabbitMQ
```
###### Docker
- ~~```docker-compose up```~~
---
#### Technologies

###### .NET 8
```sh
- ASP.NET MVC Core
- ASP.NET WebApi
- Background Services
- Entity Framework Core
```    

###### Components / Services
```sh
- RabbitMQ
- Refit 
- FluentValidator
- MediatR
- Swagger UI
```        
- ~~```Polly```~~
- ~~```Dapper```~~
- ~~```Bogus```~~

###### Architecture:
```sh
- Clean Code
- Clean Architecture
- DDD - Domain Driven Design
- Domain Events
- Domain Notification
- Domain Validations
- CQRS
- Repository
- Unit of Work
- Soft Delete
```        
- ~~```Retry Pattern```~~
- ~~```Circuit Breaker```~~

###### Plus
```sh
- Http Client Factory
- Output Cache
- CORS (FrontEnd)
- Rate Limiter
```

###### Tests
- ~~```XUnit```~~
- ~~```Bogus```~~
- ~~```Mock```~~
- ~~```Traits```~~
- ~~```Fixtures```~~
- ~~```Fluent Assertions```~~
- ~~```Coverlet```~~
---
#### Architecture Overview

#### About

developed by [Calazans](https://rcalazansn.azurewebsites.net) <img alt="Brasil" src="https://user-images.githubusercontent.com/5068797/161345649-c7184fdc-2bc3-42a9-8fb6-6ffee9c8f9c2.png" width="20" height="14" /> 


