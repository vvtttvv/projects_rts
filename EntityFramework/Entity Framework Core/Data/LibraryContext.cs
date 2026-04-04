using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Entity_Framework_Core.Models;

namespace Entity_Framework_Core.Data;

public class LibraryContext : DbContext
{
     public DbSet<Author> Authors { get; set; }
     public DbSet<Book> Books { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
         optionsBuilder.UseNpgsql(
             "Host=localhost;Database=library;Username=postgres;Password=tivlvad2004");
     }
     
     // protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
     // {
     //     configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
     // }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);
     }
}