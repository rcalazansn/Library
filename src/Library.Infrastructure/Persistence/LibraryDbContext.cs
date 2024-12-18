﻿using Library.Domain.Models;
using Library.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Library.Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext
    {
        /*
          Add-Migration Initial
		  update-database

          dotnet ef migrations add Initial -p .\src\Library.API\Library.API.csproj
          dotnet ef database update -p .\src\Library.API\Library.API.csproj

          dotnet ef migrations script -p .\src\Library.API\Library.API.csproj -o .\src\Library.API\scripts\01-Library.sql -i
         */
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
               .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);


            modelBuilder.ToSnakeNames();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreateddAt") != null))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreateddAt").IsModified = false;
                    entry.Property("ModifiedAt").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
