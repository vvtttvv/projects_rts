namespace Entity_Framework_Core.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Year { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}