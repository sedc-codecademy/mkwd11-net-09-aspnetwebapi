# Back end architecture guide

1. First we create the domain models.
2. We install EntityFramework.
3. We make the DbContext.
4. We get the connection string to the Database.
5. Configure the DbContext into the Program.cs file.
6. We create a repository.
7. We need to inject the DbContext in the repository.
8. Use the repository method in a service.
9. We are making sure to return a DTO from the service method.
10. We create DTOs and Mappers.
11. We map the result or write some logic/calculations in the service method.
12. We use that service method where ? In the controller, but you have to inject the service in to the controller itself.
13. Make sure you configure the repository and service with AddTransient/AddSingleton/AddScoped in the Program.cs file.

# Extra
-When you try to make a migration or update the database make sure to check
    -The startup project
    -Where are the migrations going to be saved/default project (in the project where the DbContext is)
    -The connection string
    -The configuration in Program.cs
    -The DbContext file
    -The methods Up and Down in the migrations
    -EntityFrameworkCore, EntityFrameworkCore.Design, EntityFrameworkCore.Tools, EntityFrameworkCore.SqlServer are installed properly.