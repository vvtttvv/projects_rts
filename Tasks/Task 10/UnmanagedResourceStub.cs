namespace Task_10;

// Also AI made
public class UnmanagedResourceStub
{
    private bool _released = false;

    public UnmanagedResourceStub()
    {
        Console.WriteLine("[UnmanagedResource] Resource is blocked");
    }

    public void Release()
    {
        if (_released) return;
        _released = true;
        Console.WriteLine("[UnmanagedResource] Resource is opened");
    }
}