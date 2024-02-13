# Cards

 RESTful web service for managing cards.

## Environment Requirements

* .NET 8 SDK
* MS SQL Server v15

## Cards Api

```Cards.API``` Is a .NET 8 Web API for serving and responding to requests

```Cards.Core``` Is the business logic layer for this project and it contains Services and Service Collection Extension, Fluent validations and AutoMappper definitions

```Cards.Data``` Is the data layer for this project and it contains DBContext and Repository definitions

```Cards.Tests``` Is the unit tests for the card-related functionalities

### Follow the steps to setup the project

* Open `appsettings.Development.json` in the `Cards.Api` directory and replace `{ServerName}` in the connection string with your ms sql server name. Check sample below

```json
    "AppConnectionString": "Server={ServerName};Database=cardsstore;Integrated Security=true;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=true;Connection Timeout=30;"


```

* Create sql server database called ```cardsstore```
* Build and Run the API Service
