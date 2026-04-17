namespace Task_10;

class Program
{
    static void Main()
    {
        UsingWithUsing();

        UsingWithoutDispose();

        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    static void UsingWithUsing()
    {
        using StrangeLib lib = new StrangeLib();
        var hash = lib.Hash("cooked");
        Console.WriteLine($"UsingWithUsing Hash: {Convert.ToHexString(hash)}...");
        Console.WriteLine("Dispose() runs automatically");
    }

    static void UsingWithoutDispose()
    {
        StrangeLib lib = new StrangeLib();
        var hash = lib.Hash("reallyCooked");
        Console.WriteLine($"UsingWithoutDispose Hash: {Convert.ToHexString(hash)}...");
        Console.WriteLine("Without calling Dispose it will run finalizer which will call CleanUp(false)");
    }
}