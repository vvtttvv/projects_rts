namespace Task9;

abstract class Program
{
    static void Main()
    {
        string[] strArr = new[] { "Vlad", "Mustang", "Alex", "Vlad", "Dima", "Tesla", "IForest OPula" }; 
        // Написать обобщённый метод расширения для массивов, который возвращает подмассив указанной длины начиная с указанного индекса. 
        string[] subArr = GetSubArray(strArr, 0, 8);
        foreach (var el in subArr)
        {
            Console.WriteLine(el);
        }
        
        Console.WriteLine();
        try
        {
            Level1();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.CheckCondition<FormatException>());   // true
            Console.WriteLine(ex.CheckCondition<InvalidOperationException>()); // true
            Console.WriteLine(ex.CheckCondition<ArgumentException>()); // false
        }
        
        //Метод принимает коллекцию объектов типа object. Коллекция может содержать любые значения, в том числе - null. 
        //Из коллекции отобрать строковые представления чисел, значения которых больше 62. Метод должен возвращать массив таких чисел. 
        Console.WriteLine();
        List<object> thirdList = new List<object>() {"Another String", null, "80", "Mystring", null, 15.5, 70, "70"};
        object[] resultingArray = FilterList(thirdList);
        foreach (var el in resultingArray)
        {
            Console.WriteLine(el);
        }
        
        // Метод принимает список строк. Из них отбирает те, которые представляют собой 
        // существующие директории и возвращает имена файлов в этих директориях (не пути). 
        List<string> fourthList = new List<string>() {"Another String", "D:\\Practice\\Tasks\\Task9\\Test", 
            "Another String", "D:\\Practice\\Tasks\\Task9\\Test"};
        string[] files = GetFiles(fourthList);
        Console.WriteLine();
        foreach (var el in files)
        {
            Console.WriteLine(el);
        }
    }

    static string[] GetFiles(List<string> list)
    {
        var result = list
            .Where(el => Directory.Exists(el))
            .SelectMany(el => Directory.GetFiles(el))
            .Select(Path.GetFileName);
            
        return result.ToArray();
    }

    static object[] FilterList(List<object> list)
    {
        int val;
        var result = list
            .Where(el => el is string && Int32.TryParse((string)el, out val) && val>62);
            
        return result.ToArray();
    }

    static void Level1()
    {
        try
        {
            Level2();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Level1 error", ex);
        }
    }

    static void Level2()
    {
        throw new FormatException("Wrong format");
    }

    public static string[] GetSubArray(string[] arr, int index, int length)
    {
        var newArr = arr
            .Select((el, i) => new { el, i })
            .Where((element) => element.i >= index && element.i <= length + index - 1)
            .Select((el) => el.el);
        // Possible solution: var newArr = arr.Skip(index).Take(length);
        return newArr.ToArray();
    }
}

// Написать метод расширения для типа Exception, который просматривает все внутренние исключения
// (и внутренние исключения внутренних исключений). Должен возвращать true, если хотя бы одно из этих исключений имеет
// тип, совместимый (такой же или производный) с типом-параметром метода, иначе - false. 
public static class ExceptionExtension
{
    public static bool CheckCondition<T>(this Exception e)
    {
        if(e is T) return true;
        while (e.InnerException != null)
        {
            if(e.InnerException is T) return true;
            e = e.InnerException;
        }

        return false;
    }
}
