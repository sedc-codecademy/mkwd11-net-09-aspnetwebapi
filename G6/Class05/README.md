# Workshop 
## Part 1
We need to create an API that keeps and manages our favorite movies. It should have the option to:

A movie contains:
* id (integer) 
* title (string) - required field
* description (string)
* year (integer) - required field
* genre (Enum) - required field

* keep data in a fixed static class

Controller:
* get movie by id (two methods: route param and query string)
* get all movies 
* create new movie
* update a movie
* delete a movie (two methods: get the id from body, get the id from route)
* filter movies by genre and/or year



## Use DTOs

## All fields except description are required. If description is entered maximum length is 250 characters.

## Bonus

Architecture:
* Domain (ClassLibrary)
* DataAccess (ClassLibrary)
    Repository (ClassLibrary)
* Mappers (ClassLibrary)
* DTOs (ClassLibrary)
* Service (ClassLibrary)
* Application (Controller)

