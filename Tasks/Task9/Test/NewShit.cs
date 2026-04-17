namespace Task9.Test;

public static class NewShit
{
    public static int ToInt(this string str)
    {
        var re = 
        int result;
        bool isSuccess = int.TryParse(str, out result);
        if (isSuccess) return result;
        Console.WriteLine(isSuccess);
        return 0;
    }
}