# Workshop - Movies App

## Part 1
We need to create an API that keeps and manages our favorite movies. It should have the option to:

* keep data in a fixed static class
* get movie by id (two methods: route param and query string)
* get all movies 
* filter movies by genre and/or year
* create new movie
* update a movie
* delete a movie (two methods: get the id from body, get the id from route)

A movie contains:

* id: integer
* title: string required field
* description: string 
* year: int - required field
* genre: enum - required field

A genre contains:

* Comedy
* Action
* Thriller

## Good to have

### Use DTO models for the responses. Use extension methods if possible for the mappings.
### All fields except description are required. 
### Length of description should be maximum of 250 characters.
### Return proper status codes for all the posible scenarios.

### Make sure every single API endpoint works. Try to use Postman :)

## Part 2

* Refactor SEDC.NoteApp to implement 3 tier architecture
* Connect SEDC.NoteApp with SQL database using Entity Framework
* Use Dependency Injection
* Verify that API enpoints are actually connected with the database using postman or swagger

## Part 3

Create an end-to-end implementation for Users. A User has:

* Username
* Password ( Hashed )
* First Name
* Last Name
* Favorite Genre
* MoviesList

* Create the following endpoints for users:

- Rgister
- Login
- Reset Password

* Implement authentication/authorization with tokens and authorize endpoints accordingly

* Extends GetAllMovies methods to return movies for specific user

