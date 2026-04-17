
static class ShowEnvironment
{
    public static void GetInfo()
    { 
        Console.WriteLine(Environment.OSVersion);
        Console.WriteLine(Environment.GetLogicalDrives());
        Console.WriteLine(Environment.Version);
    }
}