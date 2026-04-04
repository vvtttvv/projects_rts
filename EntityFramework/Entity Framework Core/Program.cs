using Entity_Framework_Core.Data;
using Entity_Framework_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Core;

public class Program
{
    public static void Main()
    {
        using var context = new LibraryContext();
        context.Database.EnsureCreated();

        context.Authors.AddRange(
                new Author {Country = "Canada", Name = "Peater"},
                new Author {Country = "Moldova", Name = "Belii"}
            );
        context.Books.AddRange(
                new Book{ Title = "How I RTSed", AuthorId = 3, Year = new DateOnly(2025, 10, 12)},
                new Book{ Title = "How I Forest", AuthorId = 3, Year = new DateOnly(2025, 10, 12)},
                new Book{ Title = "Canada is beautiful", AuthorId = 2, Year = new DateOnly(2025, 10, 12)},
                new Book{ Title = "I hate USA", AuthorId = 2, Year = new DateOnly(2024, 10, 14)}
            );
        context.SaveChanges();

        var authors = context.Authors.Include(a => a.Books).ToList();
        foreach (var author in authors)
        {
            Console.WriteLine(author.Name);
        }
    }
}