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

[Postman Collection](./Logicea%20Coding%20Challenge.postman_collection.json)

## Auth Endpoints

 ### Login

 | **HTTP Version** | **HTTP/1.1**               |
| ---------------- | -------------------------- |
| **API TYPE**     | REST/JSON                  |
| **HTTP Method**  | POST                       |
| **End-point**    |  {{baseUrl}}/api/Auth |

### Login Parameters
| Tag                       | Data Type | Required                                     | Description                        |
| ------------------------- | --------- | -------------------- | -------------------- |
| email  |   string  |         Y    |  user email      |
| password  |   string  |         Y    |   user password     |
 
 ### Sample Request

 ```json
{
  "email": "sstuart9@geocities.com",
  "password": "*******"
}
 ```
 
 ### Sample Response

 ```json
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "message": "Successful"
}
 ```

## Manage Card Endpoints

 ### Search Cards
 Filters include name, color, status and date of creation.
 Optionally limit results using page and size
 Results may be sorted by name, color, status, date of creation

| **HTTP Version** | **HTTP/1.1**               |
| ---------------- | -------------------------- |
| **API TYPE**     | REST/JSON                  |
| **HTTP Method**  | POST                       |
| **End-point**    |  {{baseUrl}}/api/Cards?name=<string>&color=<string>&status=InProgress&createdDate=<dateTime>&sortBy=<string>&orderBy=<string>&page=<integer>&size=<integer> |

| Tag         | Data Type    | Required | Data Format|
| ----------- | ------------ | -------- | ----------------------- |
| Authorization| String| yes      | Bearer **Token received from auth API on Successful Request** |

#### Search Cards Parameters
| Tag                       | Data Type | Required                                     | Description                        |
| ------------------------- | --------- | -------------------- | -------------------- |
| name  |   string  |         N    |  Filter parameter     |
| color  |   string  |         N    |   Filter parameter     |
| status  |   string  |         N    |   Filter parameter     |
| createdDate  |   string  |         N    |   Filter parameter     |
| sortBy  |   string  |         N    |   Sorting parameter  can be name, color, status or createdDate   |
| orderBy  |   string  |         N    |   Sorting parameter Can be DESC or ASC    |
| page  |   integer  |         N    |  Pagination parameter      |
| size  |   integer  |         N    |   Pagination parameter     |
 
#### Sample Request

{{baseUrl}}/api/Cards?sortBy=color&orderBy=createdDate
 
#### Sample Response

```json
[
    {
        "id": "91b6fa41-0ecd-49bd-15af-08dc2cb961ba",
        "name": "Colman",
        "description": "Wilcinskis",
        "color": "#5e8a5f",
        "status": "ToDo",
        "createdDate": "2024-02-13T20:29:50.0414829"
    },
    {
        "id": "25a9c45d-46d6-43cd-c35c-08dc2d5f0c8b",
        "name": "Trucado",
        "description": "Trucado Wilcinskis",
        "color": "#5e8a63",
        "status": "Done",
        "createdDate": "0001-01-01T00:00:00"
    },
    {
        "id": "503ed8ab-29e9-493a-15a9-08dc2cb961ba",
        "name": "Valene",
        "description": "Langthorn",
        "color": "#d3bcee",
        "status": "Done",
        "createdDate": "2024-02-13T20:29:50.0414794"
    }
]
```

---
 ### Get a Single Card
 
 Get a Single Card

| **HTTP Version** | **HTTP/1.1**               |
| ---------------- | -------------------------- |
| **API TYPE**     | REST/JSON                  |
| **HTTP Method**  | GET                       |
| **End-point**    |  {{baseUrl}}/api/Cards/:id |

| Tag         | Data Type    | Required | Data Format|
| ----------- | ------------ | -------- | ----------------------- |
| Authorization| string | yes      | Bearer **Token received from auth API on Successful Request** |


#### Delete a Card Parameters
| Tag                       | Data Type | Required                                     | Description                        |
| ------------------------- | --------- | -------------------- | -------------------- |
| id  |   uuid/string  |         Y    |  Filter parameter     |

 
#### Sample Request

{{baseUrl}}/api/Cards/91b6fa41-0ecd-49bd-15af-08dc2cb961ba
 
#### Sample Response

```json
{
    "id": "91b6fa41-0ecd-49bd-15af-08dc2cb961ba",
    "name": "Colman",
    "description": "Wilcinskis",
    "color": "#5e8a5f",
    "status": "ToDo",
    "createdDate": "2024-02-13T20:29:50.0414829"
}
```

---

 ### Add a Card
 
 Add a card

| **HTTP Version** | **HTTP/1.1**               |
| ---------------- | -------------------------- |
| **API TYPE**     | REST/JSON                  |
| **HTTP Method**  | POST                       |
| **End-point**    |  {{baseUrl}}/api/Cards |

| Tag         | Data Type    | Required | Data Format|
| ----------- | ------------ | -------- | ----------------------- |
| Authorization| String| yes      | Bearer **Token received from auth API on Successful Request** |


#### Add a Card Parameters
| Tag                       | Data Type | Required                                     | Description                        |
| ------------------------- | --------- | -------------------- | -------------------- |
| name  |   string  |         Y    |  Card name     |
| color  |   string  |         N    |   Card color     |
| description  |   string  |         N    |   description      |

 
#### Sample Request

```json
{
    "name": "Trucado II",
    "description": "Trucado Wilcinskis II",
    "color": "#5e8a63"
}
```
 
#### Sample Response

```json
{
    "id": "ce292fc0-fccb-4737-37e7-08dc2d7a6206",
    "name": "Trucado II",
    "description": "Trucado Wilcinskis II",
    "color": "#5e8a63",
    "status": "ToDo",
    "createdDate": "2024-02-14T19:31:25.3894557+03:00"
}
```
---

 ### Update a Card
 
 Update a card

| **HTTP Version** | **HTTP/1.1**               |
| ---------------- | -------------------------- |
| **API TYPE**     | REST/JSON                  |
| **HTTP Method**  | PATCH                       |
| **End-point**    |  {{baseUrl}}/api/Cards/:id |

| Tag         | Data Type    | Required | Data Format|
| ----------- | ------------ | -------- | ----------------------- |
| Authorization| String| yes      | Bearer **Token received from auth API on Successful Request** |


#### Update a Card Parameters
| Tag                       | Data Type | Required                                     | Description                        |
| ------------------------- | --------- | -------------------- | -------------------- |
| name  |   string  |         N    |  Filter parameter     |
| color  |   string  |         N    |   Filter parameter     |

 
#### Sample Request

{{baseUrl}}/api/Cards/91b6fa41-0ecd-49bd-15af-08dc2cb961ba

```json

    {
        "name": "Trucado",
        "description": "Trucado Wilcinskis",
        "color": "#5e8a63",
        "status": "Done"
    }
```
 
#### Sample Response

```json
{
    "id": "25a9c45d-46d6-43cd-c35c-08dc2d5f0c8b",
    "name": "Trucado",
    "description": "Trucado Wilcinskis",
    "color": "#5e8a63",
    "status": "Done"
}
```

---

 ### Delete a Card
 
 Delete a Card

| **HTTP Version** | **HTTP/1.1**               |
| ---------------- | -------------------------- |
| **API TYPE**     | REST/JSON                  |
| **HTTP Method**  | DELETE                       |
| **End-point**    |  {{baseUrl}}/api/Cards/:id |

| Tag         | Data Type    | Required | Data Format|
| ----------- | ------------ | -------- | ----------------------- |
| Authorization| string | yes      | Bearer **Token received from auth API on Successful Request** |


#### Delete a Card Parameters
| Tag                       | Data Type | Required                                     | Description                        |
| ------------------------- | --------- | -------------------- | -------------------- |
| id  |   uuid/string  |         Y    |  Filter parameter     |

 
#### Sample Request

{{baseUrl}}/api/Cards/91b6fa41-0ecd-49bd-15af-08dc2cb961ba
 
#### Sample Response

```json
{
    "message": "Delete Failed"
}
```

---
