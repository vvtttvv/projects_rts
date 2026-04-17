using System.Security.Cryptography;

namespace Task_10;

public class StrangeLib : IDisposable
{
    private bool _disposed = false;
    
    // This part I created with AI to simulate a real work with undetermined functionality
    // Управляемый ресурс (реализует IDisposable сам)
    private SHA256 _sha256 = SHA256.Create();
    private UnmanagedResourceStub _unmanagedResource = new UnmanagedResourceStub();

    public byte[] Hash(string input)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return _sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
    }

    
    
    public void Dispose()
    {
        CleanUp(true);
        GC.SuppressFinalize(this);
    }

    ~StrangeLib()
    {
        CleanUp(false);
    }

    private void CleanUp(bool decision)
    {
        Console.WriteLine($"CleanUp called with {decision}");
        if (_disposed == false)
        {
            if (decision)
            {  
                _sha256.Dispose();
                Console.WriteLine("[CleanUp] Manageable resource (SHA256) is collected");
            }
            
            _unmanagedResource.Release();
            Console.WriteLine($"[CleanUp] Unmanaged resource collected (disposing={decision})");
        }

        _disposed = true;
    }
}