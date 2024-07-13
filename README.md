# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
DotNet
1. dotnet dev-certs https --clean
2. dotnet dev-certs https --trust

EF Core
1. dotnet tool install --global dotnet-ef

Sql Server Docker
1. docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server
2. Server=localhost,1433;Database=Library;User ID=sa;Password=1q2w3e4r@#$
2.1. Server=localhost,1433;Database=Library;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;

RabbitMQ
1. docker run -d --hostname rabbit-local --name library-rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=library -e RABBITMQ_DEFAULT_PASS=library!@# rabbitmq:3-management
2. http://localhost:15672


docker run -d --hostname rabbit-local --name library-rabbitmq -p 5672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=library -e RABBITMQ_DEFAULT_PASS=library!@# rabbitmq:3-management





# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 
