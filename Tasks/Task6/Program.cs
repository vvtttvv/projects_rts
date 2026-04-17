using System.Collections;

namespace Task6;

/*
 * Реализовать метод, который принимает число и возвращает количество элементов последовательности Фибоначчи,
 * соответствующее этому числу. Для последовательности не должно использоваться промежуточных коллекций.
 *
 * Реализовать метод, принимающий 2 операнда и возвращающий отрицательное значение, если левый меньше второго, 0 -
 * если эквивалентны, положительное - если левый больше второго. Метод должен уметь сравнивать любые числа
 * (int, double и т.п.), строки, символы.
 */

public abstract class Program
{
    public static void Main(string[] args)
    {
        GetFibonacciCollection(7);
        
        int result = CompareOperands("A", "B");
        Console.WriteLine("\n" + result);
    }

    public static void GetFibonacciCollection(int count)
    {
        Fibonacci fci = new Fibonacci(count);
        foreach (object el in fci)
        {
            Console.WriteLine(el);
        }
    }

    public static int CompareOperands(object i, object j)
    {
        try
        {
            Comparer comp = new Comparer();
            return ((IComparer)comp).Compare(i, j);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}