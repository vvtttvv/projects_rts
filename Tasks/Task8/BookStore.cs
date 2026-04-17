namespace Task8;


public class BookStore
{
    private List<int> _booksId = new List<int>();
    private Dictionary<int, string> _orders = new Dictionary<int, string>();

    public void AddOrder(string name, int id)
    {
        _orders.Add(id, name);
    }
    
    public event EventHandler<BookEventArgs> BookArrived;

    public void NewArrival()
    {
        for (int i = 0; i < 5; i++)
        {
            int bookId = _orders.Count + i;
            _booksId.Add(bookId);
        
            if (_orders.ContainsKey(bookId))
            {
                BookArrived?.Invoke(this, new BookEventArgs { BookId = bookId, CustomerName = _orders[bookId] });
            }
        }
    }
}

public class BookEventArgs : EventArgs
{
    public int BookId { get; set; }
    public string CustomerName { get; set; }
}