namespace Task2;

using System.IO;
using System.Text;
using System.Text.RegularExpressions;

/*
•	Считать из файла текст. В тексте удалить лишние пробелы, т.е. если в исходном тексте пробел встречается 4 раза, в 
результирующем должен остаться один. Записать полученный текст в другой файл. 
•	Считать файл с кодом и проверить соблюдение парности фигурных скобок. То есть не просто проверить, что кол-во 
совпадает, но и что они верно открыты / закрыты. То есть }}{{ или {}}{ - неправильно, {{}} или {}{} - правильно. 
•	Запросить у пользователя ввод двумерного массива, повернуть его на 90 градусов по часовой стрелке и распечатать. 
•	Продемонстрировать разницу в поведении модификаторов ref, out, а также при отсутствии модификатора для ссылочных и 
значимых типов. 
•	Написать метод, который принимает строку и необязательный параметр, указывающий вариант её модификации. В 
зависимости от значения параметра строка может переводиться в верхний или нижний регистр. Если параметр не задан, 
возвращается исходная строка. 
•	Объявить тип, который описывает человека (Person) - содержит имя, фамилию и возраст. Показать, как сделать этот 
тип значимым, как - ссылочным. Примеры работы с экземплярами этого типа. 
•	Написать пример, в котором возникает StackOverflowException. Обосновать его возникновение. 

 */
public class Program
{
    public static void Main(string[] args)
    {
        //Считать из файла текст. В тексте удалить лишние пробелы, т.е. если в исходном тексте пробел встречается 4 раза, в 
        //результирующем должен остаться один. Записать полученный текст в другой файл. 
        StringBuilder sb = new StringBuilder();
        try
        {
            StreamReader sr = new StreamReader("D:\\Practice\\Tasks\\Task2\\text.txt");
            String? line = sr.ReadLine();
            while (line != null)
            {
                sb.Append(Regex.Replace(line, @"\s+", " ") + "\n");
                line = sr.ReadLine();
            }
            sr.Close();
            StreamWriter sw = new StreamWriter("D:\\Practice\\Tasks\\Task2\\result.txt");
            sw.Write(sb.ToString());
            sw.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        // Считать файл с кодом и проверить соблюдение парности фигурных скобок. То есть не просто проверить, что кол-во 
        // совпадает, но и что они верно открыты / закрыты. То есть }}{{ или {}}{ - неправильно, {{}} или {}{} - правильно. 
        int counter = 0;
        try
        {            
            StreamReader sr = new StreamReader("D:\\Practice\\Tasks\\Task2\\code.txt");
            String? line = sr.ReadLine();
            while (line != null)
            {
                foreach (char c in line)
                {
                    if (c == '{')
                    {
                        counter++;
                    }
                    else if (c == '}')
                    {
                        counter--;
                        if (counter < 0)
                        {
                            Console.WriteLine("Brackets are not paired");
                            break;
                        }
                    }
                }
                line = sr.ReadLine();
            }
            sr.Close();
            if (counter == 0)
            {
                Console.WriteLine("Brackets are paired");
            }
            else
            {
                Console.WriteLine("Brackets are not paired, " + counter);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        // Запросить у пользователя ввод двумерного массива, повернуть его на 90 градусов по часовой стрелке и распечатать. 
        Console.WriteLine("Please, provide the number of rows and columns of the array:");
        int rows = int.Parse(Console.ReadLine() ?? "1");
        int cols = int.Parse(Console.ReadLine() ?? "1");
        int[,] arr = new int[rows, cols];
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < cols; i++)
            {
                arr[i, j] = Int32.Parse(Console.ReadLine()??"0");
            }
        }
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(arr[i, j] + " ");
            }
            Console.WriteLine();
        }
        
        //Продемонстрировать разницу в поведении модификаторов ref, out, а также при отсутствии модификатора для ссылочных и значимых типов. 
        int value = 5;
        string str = "Hello";
        Console.WriteLine($"Values before method call: {value}, {str}");
        ChangeValue(value, str);
        Console.WriteLine($"Values after method call: {value}, {str}");
        ChangeValue(ref value, ref str);
        Console.WriteLine($"Values after method call with ref: {value}, {str}");
        int newVal;
        string newStr;
        ChangeValueOut(out newVal, out newStr);
        Console.WriteLine($"Values after method call with out: {newVal}, {newStr}");
        
        //Написать метод, который принимает строку и необязательный параметр, указывающий вариант её модификации. В 
        //зависимости от значения параметра строка может переводиться в верхний или нижний регистр. Если параметр не задан, 
        //возвращается исходная строка. 
        string inputStr = "Hello World!";
        ChangeString(inputStr, State.Upper);
        
        //Объявить тип, который описывает человека (Person) - содержит имя, фамилию и возраст. Показать, как сделать этот 
        //тип значимым, как - ссылочным. Примеры работы с экземплярами этого типа.
        PersonStruct p1 = new PersonStruct("Daniel", "Johnson", 30); // value type
        PersonStruct p2 = p1;
        PersonClass p3 = new PersonClass("Emily", "Smith", 25); // reference type
        PersonClass p4 = p3;
        p2.Age = 99;
        p4.Age = 99;
        Console.WriteLine($"p1: {p1.Name} {p1.Surname} {p1.Age}");
        Console.WriteLine($"p3: {p3.Name} {p3.Surname} {p3.Age}");

        // Написать пример, в котором возникает StackOverflowException. Обосновать его возникновение. 
        // try
        // {
        //     RecursiveMethod();
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine($"Error occured, this is the problem: {e}");
        //     throw;
        // }
        
        

        // object[] arr = new object[] { 1, 2, 3, "g" };
        // foreach (object el in arr)
        // {
        //     Console.WriteLine("{0} {1}", el, el.GetType());
        // }
        // Console.WriteLine(arr);
        // int[][] jagArr = new int[8][];
        // for (int i = 0; i < 8; i++)
        // {
        //     jagArr[i] = new int[i + 1];
        //     for (int j = 0; j < jagArr[i].Length; j++)
        //     {
        //         Console.Write(jagArr[i][j] + " ");
        //     }
        //
        //     Console.WriteLine();
        // }
        //
        //     int newVal = 5;
        //     ChangeValue(out newVal);
        //     Console.WriteLine(newVal);
        //     string s1 = "Hello";
        //     string s2 = "World";
        //     ChangeValueWithRef(ref s1, ref s2);
        //     Console.WriteLine($"s1: {s1}, s2: {s2}");
        //     
        //     DayOfWeek day = DayOfWeek.Wednesday;
        //     EvaluateEnum(day);
        //     
        //     Console.WriteLine ("***** Fun with Nullable Data *****\n");
        //     DatabaseReader dr = new DatabaseReader (); 
        //     int? i = dr.GetIntFromDatabase() ;
        //     if (i.HasValue)
        //         Console.WriteLine("Value of ' i' is: {0}", i.Value); 
        //     else
        //         Console.WriteLine("Value of 'i' is undefined."); 
        //     bool? b = dr.GetBoolFromDatabase();
        //     if (b != null)
        //         Console.WriteLine("Value of ’b' is: {0}", b.Value); 
        //     else
        //         Console.WriteLine("Value of ’b’ is undefined.");
        //
        //
        //     Point p = new Point(7, 5);
        //     var pointValues = p;
        //     Console.WriteLine(pointValues.X);
        //
        // }
        //
        // static void EvaluateEnum(System.Enum e)
        // {
        //     Console.WriteLine("=> Information about {0}", e.GetType().Name);
        //     Console.WriteLine("Underlying storage type: {0}",
        //         Enum.GetUnderlyingType(e.GetType()));
        //     Array enumData = Enum.GetValues(e.GetType ());
        //     Console.WriteLine("This enum has {0} members.", enumData.Length);
        //     for(int i=0; i < enumData.Length; i++)
        //     {
        //         Console.WriteLine("Name : {0}, Value: {0:D}",
        //             enumData.GetValue(i));
        //     }
        //     Console.WriteLine ();
        // }
        //
        // static void ChangeValue(out int val)
        // {
        //     val = 10;  
        // }
        //
        // static void ChangeValueWithRef(ref string s1, ref string s2)
        // {
        //     (s1, s2) = (s2, s1);
    }
    
    static void RecursiveMethod()
    {
        RecursiveMethod(); 
    }
    
    static void ChangeValue(int val, string str)
    {
        val = 10;
        str = "World";
    }
    
    static void ChangeValue(ref int val, ref string str)
    {
        val = 10;
        str = "World";
    }
    
    static void ChangeValueOut(out int val, out string str)
    {
        val = 10;
        str = "World";
    }

    static void ChangeString(string str, State state = State.Default)
    {
        switch (state)
        {
            case State.Upper:
                Console.WriteLine(str.ToUpper());
                break;
            case State.Lower:
                Console.WriteLine(str.ToLower());
                break;
            default:
                Console.WriteLine(str);
                break;
        }
    }
}

struct PersonStruct
{
    PersonClass personClass;
    public string Name;
    public string Surname;
    public int Age;
    
    public PersonStruct(string name, string surname, int age)
    {
        Name = name;
        Surname = surname;
        Age = age;
    }
}

class PersonClass
{
    public string Name;
    public string Surname;
    public int Age;

    public PersonClass(string name, string surname, int age)
    {
        Name = name;
        Surname = surname;
        Age = age;
    }
}

enum State
{
    Upper,
    Lower,
    Default
}


// enum DayOfWeek
// {
//     Sunday = 115,
//     Monday,
//     Tuesday,
//     Wednesday,
//     Thursday,
//     Friday,
//     Saturday
// }
//
// struct Point
// { 
//     public int X;
//     public int Y; 
//     public Point(int XPos, int YPos)
//     {
//         X = XPos;
//         Y = YPos;
//     }
//     public (int XPos, int YPos) Deconstruct() => (X, Y);
// }
