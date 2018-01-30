# Delivery Service API

This repository shows an example of a cross platform delivery service API that picks the best route between two places based on a cost (cost or time).
Additionally has all CRUD methods to manage routes by an Admin user.

The algorithm for picking the best route is explained here (wikipedia): [Dijkstra's algorithm](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)

1. Domain Driven Design
2. ASP NET CORE Web API
3. Entity Framework Core (SQL Server)
4. Identity Server 4
5. Swagger UI
6. Generic Repository pattern
7. Unit of Work pattern
8. Dependency Injection
9. AutoMapper
10. Unit Testing (xUnit)

## Requirements

1. .Net Core 2.0
2. Visual Studio 2017 or Visual Studio Code


## Solution Description

This project has different layers using Domain Driven Design approach:
> This is a simple implementation of DDD

#### Business Tier
  1. `Domain.Core`: Base contracts to support the domain's implementation (Model, Repositories and Services);
  2. `Domain.Model`: Implementation of the project's entities;
  3. `Domain.Services`: Contracts and implementation of the RoutesService and the Routing algorithm;
  4. `Domain.Tests`: Unit Tests of the Domain Model;

#### Infrastructure Tier
  1. `Infrastructure`: Handles the connection to the database. Implementation of Unit of Work and Repositories using Entity Framework Core;

#### Services Tier
  1. `Services.IdentityServer`: Server to authenticate and generate tokens for authorization purposes (OpenId and OAuth2);
  2. `Services.WebApi`: Implementation of the Delivery Service API and HTTP server (Kestrel);

#### IoC
  1. `IoC`: Dependency Injection: Project to handle the registraction of all dependencies between projects;

#### Clients Tier
  1. `Clients.ConsoleClient`: Simple test console to call the API.


## How to use

1. Change the ConnectionString in the appsetting.json in project `DeliveryService.Services.WebApi`;
2. Build and Run the project `DeliveryService.Services.IdentityServer` on http://localhost:5000;
3. Build and Run the project `DeliveryService.Services.WebApi` on http://localhost:5001;
4. In a browser Navigate to http://localhost:5001/Swagger to check the documentation and test the API (it has nice forms to test all the routes).
   On the top right corner there's an `Authorize` button that enables the possibility to log in to test the protected routes (redirect to Identity Server to exchange tokens);
   
   - **User:** filipe (admin user) | **Password:** password
   - **User:** bob (normal user) | **Password:** password
   
5. There's a Console Application (project `DeliveryService.Clients.ConsoleClient`) that tests two calls to the API and tests a call to a protected route using the Admin user and the normal user;

## Available Routes

#### Protected routes (Admin only)
- GET api/routes  : Gets all available routes;
- POST api/routes : Add a new route;
- PUT api/routes  : Update a route;
- DELETE api/routes : Delete a route;

#### Public routes
- GET api/routes/best : Gets the best route between two places based on criteria (time or cost);

#### Example of usage:
- GET api/routes/best?origin=[origin name]&destination=[destination name]&criteria=[time or cost]
- GET api/routes/best?origin=A&destination=B&criteria=time

## Notes

> The Identity Server is for tests only, it uses test clients, resources and test users. Is not supposed to use this configuration directly in production.
Every one of those resources should come from a DB.

> Every address should come from a config file or database when sending in to production.


