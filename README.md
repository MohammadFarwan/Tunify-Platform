# Tunify Platform


## Overview

Lab 11 focuses on integrating your database tables into your Tunify Platform web application. This lab guides you through setting up the database with Entity Framework Core, creating migrations, defining models, and seeding initial data.

## Entity-Relationship Diagram (ERD)

![ERD Diagram](images/Tunify.png)


## Instructions

### Application Setup

1. **Initial Configuration**
   - Create a new Empty .NET Core Web Application.
   - Install the following NuGet packages:
     - `Microsoft.EntityFrameworkCore.SqlServer v7.0.20`
     - `Microsoft.EntityFrameworkCore.Tools v7.0.20`
     - `Microsoft.VisualStudio.Web.CodeGeneration.Design v7.0.12`
   - Register your `DbContext` in the `ConfigureServices` method of your `Startup` class.
   - Add your connection string to the `appsettings.json` file:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TunifyDB;Trusted_Connection=true;TrustServerCertificate=True;MultipleActiveResultSets=true"
     }
     ```

### Defining Models & Setting Up the Database

1. **Create Models**
   - Create a folder named `Models` and add your entity classes as defined in your ERD.
   - Start with a `User` model and define its properties.

2. **Setup DbContext**
   - Create a new folder named `Data` and add a file `TunifyDbContext.cs`.
   - Derive `TunifyDbContext` from `DbContext` and include a constructor that accepts `DbContextOptions<TunifyDbContext>`.

3. **Create Initial Migration**
   - Using the Terminal: `dotnet ef migrations add CreateUsersTable`
   - Using the Package Manager Console: `Add-Migration CreateUsersTable`
   - Apply the migration:
     - Using the Terminal: `dotnet ef database update`
     - Using the Package Manager Console: `Update-Database`
   - Verify the `Users` table creation in your local database.

### Adding More Models

1. **Define Additional Models**
   - Add model classes for `Subscription`, `Playlist`, `Song`, `Artist`, `Album`, and `PlaylistSongs` in the `Models` folder.
   - Define their properties according to your ERD.

2. **Update DbContext**
   - Update `TunifyDbContext` to include `DbSet` properties for the new models:
     ```csharp
     public DbSet<Song> Songs { get; set; }
     ```

3. **Create and Apply Migrations**
   - Create migration for new tables:
     - Using the Terminal: `dotnet ef migrations add CreateMusicTables`
     - Using the Package Manager Console: `Add-Migration CreateMusicTables`
   - Apply the migration:
     - Using the Terminal: `dotnet ef database update`
     - Using the Package Manager Console: `Update-Database`
   - Confirm the new tables are created in the database.

### Seeding Initial Data

1. **Seed Data**
   - Override `OnModelCreating` in `TunifyDbContext` to seed initial data for `User`, `Song`, and `Playlist`:
     ```csharp
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.Entity<User>().HasData(
             // Seeded Data
         );
     }
     ```

2. **Create and Apply Seed Data Migration**
   - Create a migration for seed data:
     - Using the Terminal: `dotnet ef migrations add SeedInitialData`
     - Using the Package Manager Console: `Add-Migration SeedInitialData`
   - Apply the migration:
     - Using the Terminal: `dotnet ef database update`
     - Using the Package Manager Console: `Update-Database`
   - Confirm the initial data is seeded.

## Final Steps

- Ensure your database and models are set up correctly and seeded with initial data.
- Update this `README.md` file with:
  - A brief description of the Tunify Platform.
  - The Tunify ERD Diagram.
  - An overview of the relationships and how each entity is related to another.


