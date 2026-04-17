namespace  Task1;

using System;
using System.Text;
using System.IO;

/*
  This program is created in order to implement the following tasks:
•	Распечатать информацию о машине. Использовать для этого форматные и интерполированные строки. ✓
•	Запросить у пользователя имя, фамилию и год рождения. Вывести информацию - имя и его возраст. 
•	Вывести аргументы командной строки, переданные приложению, в обратном порядке. Реализация минимум с 2 разными 
циклами. 
•	Спросить у пользователя, хочет ли он записать текст. В случае положительного ответа считать строку, сохранить. 
Повторять вопрос и считывание до отрицательного ответа. После вывести весь введённый текст. 
•	Приложение принимает на вход путь до директории и распечатывает структуру каталогов и фалов в них. 
*/
class Program
{
    static void Main(string [ ] args)
    {
		// Распечатать информацию о машине. Использовать для этого форматные и интерполированные строки. 
		Console.WriteLine("Machine name is: {0}", Environment.MachineName);
        Console.WriteLine("OS version is: {0}", Environment.OSVersion);
		Console.WriteLine($"{Environment.UserName} is logged in");
        Console.WriteLine($"{Environment.CpuUsage} used CPU");
        
        // Запросить у пользователя имя, фамилию и год рождения. Вывести информацию - имя и его возраст. 
        Console.WriteLine("Please provide your first name:");
        string firstName = Console.ReadLine() ?? "Unknown";
        Console.WriteLine("Please provide your last name:");
        string lastName = Console.ReadLine() ?? "Unknown";
        Console.WriteLine("Please provide your year of birth:");
        int yearOfBirth = int.Parse(Console.ReadLine() ?? "0");
        int age = DateTime.Now.Year - yearOfBirth;
        Console.WriteLine($"Your name is {firstName} {lastName} and you are {age} years old.");
        
        // Вывести аргументы командной строки, переданные приложению, в обратном порядке. Реализация минимум с 2 разными циклами.
        for (int i = args.Length; i > 0; i--)
        {
            Console.Write(" {0} ", args[i - 1]);
        }
        Console.WriteLine();
        int j = args.Length;
        while(j>0)
        {
            Console.Write($" {args[j-1]} ");
            j--;
        }
        Console.WriteLine();
        
        // Спросить у пользователя, хочет ли он записать текст. В случае положительного ответа считать строку, сохранить. 
        // Повторять вопрос и считывание до отрицательного ответа. После вывести весь введённый текст. 
        StringBuilder sb = new StringBuilder();
        string response = "yes";
        while (response.ToLower() == "yes")
        {
            Console.WriteLine("Do you want to enter some text? (yes/no)");
            response = Console.ReadLine() ?? "no";
            if (response.ToLower() == "yes")
            {
                Console.WriteLine("Please enter your text:");
                string userInput = Console.ReadLine() ?? "";
                sb.Append(userInput);
            }
        }
        Console.WriteLine($"You entered the following text:\n{sb.ToString()}");
        
        // Приложение принимает на вход путь до директории и распечатывает структуру каталогов и файлов в них. 
        // Since in the book there is no info about it I read additional sources
        Console.WriteLine("Please provide a directory path:");
        string directoryPath = Console.ReadLine() ?? "";
        if (Directory.Exists(directoryPath))
        {
            Console.WriteLine($"Directory: {directoryPath}");
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            Console.WriteLine("Subdirectories:");
            foreach (string subdir in subdirectories)
            {
                Console.WriteLine(subdir);
            }
            string[] files = Directory.GetFiles(directoryPath);
            Console.WriteLine("Files:");
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }
        else
        {
            Console.WriteLine("Directory does not exist.");
        }
        

        // It was a small playground
        /*
        ShowEnvironment.GetInfo();
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        int var = default;
        Console.WriteLine(var.GetType());
        DataTypeFunctionality();
        EscapeChars();
        StringComparisonRevised();
        StnngEqualitySpecifyingCompareRules();
        FunWithStnngBuilder();
        */
    }





/*
    static void DataTypeFunctionality()
    {
        Console.WriteLine("=> Data type Functionality:");
        Console.WriteLine("Max of int: {0}", int.MaxValue);
        Console.WriteLine("Min of int: {0}", int.MinValue);
        Console.WriteLine("Max of double: {0}", double.MaxValue);
        Console.WriteLine("Min of double: {0}", double.MinValue);
        Console.WriteLine("double.Epsilon: {0}", double.Epsilon);
        Console.WriteLine("double.PositiveInfinity: {0}", double.PositiveInfinity);
        Console.WriteLine("double.Negativelnfinity: {0}", double.NegativeInfinity);
        Console.WriteLine();
        double newVal = 123_321.125_456;
        Console.WriteLine(newVal);
    }

    static void EscapeChars()
    {
        Console.WriteLine("=> Escape characters:");
        string strWithTabs = "Model\tColor\tSpeed\tPet Name ";
        string strWithTabs2 = "fddddddddddddd\tColor\tSpeed\tPet Name ";
        Console.WriteLine(strWithTabs);
        Console.WriteLine(strWithTabs2);
        Console.WriteLine("Everyone loves V'Hello World\" ");
        Console.WriteLine("C:\\MyApp\\bin\\Debug ");
        Console.WriteLine("All finished.\n\n\n \a");
        Console.WriteLine();
        string myLongString = @"This is a very
                very
                                    very
                                    long string";
        Console.WriteLine(myLongString);
        Console.WriteLine(@"Cerebus said ""Darrr! Pret-ty sun-sets""");
    }
    
    static void StringComparisonRevised()
    {
        Console.WriteLine("=> String Comparison:");
        string s1 = "Hello!";
        string s2 = "Yo!";
        Console.WriteLine("s1 = {0}", s1);
        Console.WriteLine("s2 = {0}", s2);
        Console.WriteLine("s1 == s2: {0}", s1 == s2);
        Console.WriteLine("s1 != s2: {0}", s1 != s2);
        Console.WriteLine("s1.Equals(s2): {0}", s1.Equals(s2));
        Console.WriteLine("s1.CompareTo(s2): {0}", s1.CompareTo(s2));
        Console.WriteLine("s1 and s2 {0}", s1 == "HELLO");
        Console.WriteLine(s1 == "Helloo");
    }
    
    static void StnngEqualitySpecifyingCompareRules (){
        string s1 = "Hello!";
        string s2 = "HELLO!";

        Console.WriteLine("s1 = {0}", s1);
        Console.WriteLine("s2 = {0}", s2);
        Console.WriteLine();
        Console.WriteLine(
            "Default rules: s1={0}, s2={1}, s1.Equals(s2): {2}",
            s1, s2, s1.Equals(s2));

        Console.WriteLine(
            "Ignore case (OrdinalIgnoreCase): {0}",
            s1.Equals(s2, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(
            "Ignore case (InvariantCultureIgnoreCase): {0}",
            s1.Equals(s2, StringComparison.InvariantCultureIgnoreCase));

        Console.WriteLine();

        // IndexOf — поиск подстроки
        Console.WriteLine(
            "Default rules: s1={0}, s2={1}, s1.IndexOf(\"E\"): {2}",
            s1, s2, s1.IndexOf("E"));

        Console.WriteLine(
            "Ignore case (OrdinalIgnoreCase): {0}",
            s1.IndexOf("E", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(
            "Ignore case (InvariantCultureIgnoreCase): {0}",
            s1.IndexOf("E", StringComparison.InvariantCultureIgnoreCase));

        Console.WriteLine();
    }
    
    static void FunWithStnngBuilder ()
    {
        Console.WriteLine("=> Using the StringBuilder:");
        StringBuilder sb = new StringBuilder ("**** Fantastic Games ****");
        sb.Append("\n");
        sb.AppendLine("Half Life");
        sb.AppendLine("Morrowind");
        sb.AppendLine("Deus Ex" + "2");
        sb.AppendLine("System Shock");
        Console.WriteLine(sb.ToString());
        sb.Replace("2", " Invisible War");
        Console.WriteLine(sb.ToString());
        Console.WriteLine("sb has {0} chars.", sb.Length);
        Console.WriteLine();
    }
*/

}