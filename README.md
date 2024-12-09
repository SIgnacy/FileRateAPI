## .NET API [WIP]
.NET API built following clean architecture principles and CQRS pattern.
Designed to manage users and items, this project serves as excercise for educational purposes

## Existing endpoints
##### Members
- __GET__ /api/members/?search=term&page=1&size=10
  ###### Example response
  ```json
  {
  "items": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "username": "string",
      "displayName": "string"
    }
  ],
  "page": 1,
  "size": 10,
  "totalCount": 1,
  "hasNextPage": false,
  "hasPreviousPage": false
  }
  ``` 
- __GET__ /api/members/{id}
  ###### Example response
  ```json
  {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "username": "string",
  "displayName": "string"
  } 
  ```
- __POST__ /api/members
  ###### Example body
  ```json
  {
    "username": "string",
    "displayName": "string"
  }
  ```
- __PATCH__ /api/members
  ###### Example response
  ```json
  {
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "newUsername": "string",
  "newDisplayName": "string"
  }

- __DELETE__ /api/members/{id}

##### Keywords
- __GET__ /api/keywords/?search=term&column=popularity&order=asc&page=1&size=10
  ###### Example response
  ```json
  {
  "items": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "keyword": "string",
    }
  ],
  "page": 1,
  "size": 10,
  "totalCount": 1,
  "hasNextPage": false,
  "hasPreviousPage": false
  }
  ```

## Planned features
- Endpoints for item management
- JWT-based authentication

## Tech Stack
- .NET 8.0
- Swagger
- Entity Framework Core
- SQL Server
- MediatR
- XUnit
- Moq
