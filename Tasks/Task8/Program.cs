namespace Task8;

class Program
{
    static void Main()
    {
        List<string> stringList = new List<string>();
        for (int i = 0; i < 10; i++)
        {
            stringList.Add($"Meow{i}");
        }
        for (int i = 0; i < 3; i++)
        {
            stringList.Add($"meow{i}");
        }

        // Имеется список строк. Используя лямбда-выражения, из этого списка отобрать все строки, которые содержат только символы в нижнем регистре.
        // Using Predicate which is a simple Func as I understand
        stringList = stringList.FindAll(el => el.ToLower() == el);
        foreach (string el in stringList)
        {
            Console.WriteLine(el);
        }
        
        // Продемонстрировать работу событий на своём примере или предложенном.  
        // Возможный вариант для демонстрации работы событий: есть книжный магазин, в который поступают книги. Есть покупатели, 
        // которые подписываются на оповещение о поступлении книги. Когда соответствующая книга поступает в магазин, покупатель 
        // получает оповещение.
        BookStore bookStore = new BookStore();
        bookStore.AddOrder("John", 5);
        bookStore.AddOrder("Alex", 2);
        bookStore.BookArrived += (sender, e) =>
        {
            Console.WriteLine($"{e.CustomerName}, your book with id {e.BookId} has arrived!");
        };
        bookStore.NewArrival();
    }
    
}

