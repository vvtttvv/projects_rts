namespace Task3;

public class Person
{
    // Модифицировать тип, Person. Тип должен содержать свойства, позволяющие изменять имя, фамилию, возраст. Если через
    // свойство в имя или фамилию пытаются записать значение null, записывать пустую строку.
    private string firstName;
    private string lastName;

    public Person() { }

    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
    
    public string? FirstName
    {
        get
        {
            return firstName;
        }
        set
        {
            if (value != null)
            {
                firstName = value;
            }
            else
            {
                firstName = "";
            }
        }
    }
    
    public string? LastName
    {
        get
        {
            return lastName;
        }
        set
        {
            if (value != null)
            {
                lastName = value;
            }
            else
            {
                lastName = "";
            }
        }
    }

    public int Age { get; set; }

    
}