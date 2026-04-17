namespace Task6;

using System.Collections;

public class Comparer : IComparer
{
    int IComparer.Compare(object o1, object o2)
    {
        if(o1 is IComparable comparable)
            return comparable.CompareTo(o2);

        throw new Exception("I don't know how to compare it");
    }
}