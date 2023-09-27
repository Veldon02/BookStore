# Online Bookstore API

This is a RESTful API for a simple online bookstore built using ASP.NET Core and EF. It allows users to perform CRUD operations on books, authors, and genres, as well as search for books by title, author, or genre.

## Prerequisites

Before running this application, you will need:
- .NET Core SDK 3.1 or later
- Visual Studio or Visual Studio Code (optional)

## Running the Application

To run the application, follow these steps:
1. Clone this repository to your local machine
2. Open the solution file (`OnlineBookstore.sln`) in Visual Studio or Visual Studio Code
3. Build the solution to restore packages and dependencies
4. Open the `appsettings.json` file and modify the `ConnectionString` value to point to your local SQL Server instance
5. Open the Package Manager Console and run the following command to create the database:
```
Update-Database
```
6. Run the application by pressing F5 in Visual Studio or by running the command `dotnet run` in the terminal

The application should now be running on `http://localhost:5000`.

## API Endpoints

The following API endpoints are available:

### Books

- GET /api/books
  - Retrieves a list of all books
- GET /api/books/{id}
  - Retrieves a book by its ID
- POST /api/books
  - Adds a new book
- PUT /api/books/{id}
  - Updates a book by its ID
- DELETE /api/books/{id}
  - Deletes a book by its ID
- GET /api/books/search/title/{title}
  - Searches for books by title
- GET /api/books/search/author/{author}
  - Searches for books by author name
- GET /api/books/search/genre/{genre}
  - Searches for books by genre name

All book endpoints accept and return the following JSON format:
```
{
  "id": 1,
  "title": "The Shining",
  "author": {
    "id": 1,
    "name": "Stephen King"
  },
  "genre": {
    "id": 1,
    "name": "Horror"
  },
  "price": 10.99,
  "quantityAvailable": 5
}
```

### Authors

- GET /api/authors
  - Retrieves a list of all authors
- GET /api/authors/{id}
  - Retrieves an author by its ID
- POST /api/authors
  - Adds a new author
- PUT /api/authors/{id}
  - Updates an author by its ID
- DELETE /api/authors/{id}
  - Deletes an author by its ID

All author endpoints accept and return the following JSON format:
```
{
  "id": 1,
  "name": "Stephen King"
}
```

### Genres

- GET /api/genres
  - Retrieves a list of all genres
- GET /api/genres/{id}
  - Retrieves a genre by its ID
- POST /api/genres
  - Adds a new genre
- PUT /api/genres/{id}
  - Updates a genre by its ID
- DELETE /api/genres/{id}
  - Deletes a genre by its ID

All genre endpoints accept and return the following JSON format:
```
{
  "id": 1,
  "name": "Horror"
}
```

## Testing

To run unit tests on this application, open the `OnlineBookstore.Tests` project in Visual Studio or Visual Studio Code and run the tests using the Test Explorer or the `dotnet test` command in the terminal. These tests use mocking to avoid interacting with a real database.