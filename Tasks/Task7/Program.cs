namespace Task7;

/*
•	Реализовать свой аналог класса List. Тип должен быть обобщённым. Для списка должна быть реализована возможность его 
обхода в цикле foreach. 
•	Конструкторы:  
    o	без параметров; 
    o	принимает число, задающее ёмкость; 
    o	принимает коллекцию. 
•	Свойства: 
    o	Length - текущая длина списка; 
    o	Capacity - текущая ёмкость списка; 
•	Методы: 
    o	Add - добавляет элемент в список; 
    o	AddRange - добавляет коллекцию к списку; 
    o	Clear - очищает список; 
    o	IndexOf - возвращает индекс элемента или -1, если элемент не найден; 
    o	Remove - удаляет элемент по индексу; 
    o	RemoveAll - удаляет все элементы из списка, идентичные переданному; 
    o	Reverse - инвертирует список;
 */

public static class Program
{
    public static void Main(string[] args)
    {
        var list = new MyList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        Console.WriteLine($"Length: {list.Length}, Capacity: {list.Capacity}");

        list.Add(4);
        list.Add(5);
        Console.WriteLine($"Length: {list.Length}, Capacity: {list.Capacity}");

        Console.Write("foreach: ");
        foreach (var el in list) Console.Write(el + " ");
        Console.WriteLine();

        var list2 = new MyList<int>(10);
        Console.WriteLine($"Capacity: {list2.Capacity}");

        var list3 = new MyList<int>(new List<int> { 10, 20, 30 });
        Console.WriteLine($"Length: {list3.Length}");
        
        list.AddRange(new[] { 6, 7, 8 });
        Console.WriteLine($"Length after AddRange: {list.Length}");

        Console.WriteLine($"IndexOf(3): {list.IndexOf(3)}");
        Console.WriteLine($"IndexOf(99): {list.IndexOf(99)}");

        list.Remove(0);
        Console.Write("After Remove(0): ");
        foreach (var el in list) Console.Write(el + " ");
        Console.WriteLine();

        list.Add(3);
        list.Add(3);
        list.RemoveAll(3);
        Console.Write("After RemoveAll(3): ");
        foreach (var el in list) Console.Write(el + " ");
        Console.WriteLine();

        list.Reverse();
        Console.Write("After Reverse: ");
        foreach (var el in list) Console.Write(el + " ");
        Console.WriteLine();

        list.Clear();
        Console.WriteLine($"After Clear - Length: {list.Length}, Capacity: {list.Capacity}");
    }
}