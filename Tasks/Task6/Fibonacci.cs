using System.Collections;

namespace Task6;

public class Fibonacci(int value) : IEnumerable
{
    private readonly int _value = value > 2 ? value : 2;

    IEnumerator IEnumerable.GetEnumerator()
    {
        int f1 = 0, f2 = 1;
        for (int i = 0; i < _value; i++)
        {
            yield return f1;
            (f1, f2) = (f2, f2 + f1); // Wow, it was a guess, but it works even after return...
        }
    }
}