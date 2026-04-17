using Task5.Exceptions;

namespace Task5;

using System;

/*
 *  Модифицировать тип, описывающий человека. Конструктор должен выбрасывать исключения, если: 
    o	имя или фамилия имеют значение null; 
    o	имя или фамилия - пустые строки; 
    o	возраст лежит вне диапазона (например, отрицательный или больше 150); 
•	аналогичные исключения должны выкидываться при попытке записи соответствующих значений в свойства. 
•	Модифицировать приложение, чтобы оно перехватывало исключения, которые может выкидывать конструктор типа Person. 
•	Модифицировать приложение, чтобы оно считывало данные для создания списка пользователей, пока не получит сигнал 
остановки. После каждой попытки создания объекта нужно выводить количество успешно и неуспешно созданных объектов типа Person. 
 */

public abstract class Program
{
    public static void Main(string[] args)
    {
        List<Person> persons = new List<Person>();
        int success = 0, fail = 0;
        string decision = "";
        
        while (decision == "")
        {
            try
            {
                Console.WriteLine("Please, provide info about the user:");
                Console.Write("First name:");
                string firstName = Console.ReadLine() ?? "";
                Console.Write("Last name:");
                string lastName = Console.ReadLine() ?? "";
                Console.Write("Age:");
                int age;
                try
                {
                    age = int.Parse(Console.ReadLine() ?? "");
                }
                catch (Exception e)
                {
                    age = -1;
                }
                Person p1 = new Person(firstName, lastName, age);
                success++;
            }
            catch (EmptyDataException e)
            {
                Console.WriteLine($"Empty Data Exception: {e.Message}");
                fail++;
            }
            catch (AgeException e)
            {
                Console.WriteLine($"Age Exception: {e.Message}");
                fail++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("The iteration has ended, wanna continue? Press Enter if yes, anything if no.");
                decision = Console.ReadLine() ?? "";
            }
        }
        
        Console.WriteLine($"Succeses: {success}, Fails: {fail}");
    }
}