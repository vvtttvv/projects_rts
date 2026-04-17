namespace Task3;

using System;

public class Program
{
    public static void Main(string[] args)
    {
        List<ObjectCounter> list = new List<ObjectCounter>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(new ObjectCounter());
        }

        Console.WriteLine(ObjectCounter.Count);
        Console.WriteLine(ObjectCounter.GetInfo());
        Console.WriteLine(list);

        //Seocnd task
        Person p1 = new Person();
        Person p2 = new Person(null, "John", 10);
        p1.Age = 17;
        p1.FirstName = null;
        p1.LastName = "Somebody";
        object[] arr = new[] { new Person() };

    Console.WriteLine($"Person 1 {p1.Age}, {p1.FirstName}, {p1.LastName}");
        Console.WriteLine($"Person 2 {p2.Age}, {p2.FirstName}, {p2.LastName}");
        Console.WriteLine(arr[0] as ObjectCounter);
    }
}

