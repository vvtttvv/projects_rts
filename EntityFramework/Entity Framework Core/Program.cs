using Entity_Framework_Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Core;

public class Program
{
    public static void Main()
    {
        using var context = new LibraryContext();
        // context.Database.EnsureCreated();

        var authors = context.Authors.Include(a => a.Books).ToList();
        foreach (var author in authors)
        {
            Console.WriteLine(author.Name);
        }
    }
}