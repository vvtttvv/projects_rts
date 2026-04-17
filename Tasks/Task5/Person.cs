using Task5.Exceptions;

namespace Task5;

public class Person
{
    private string _firstName;
    private string _lastName;
    private int _age;

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
            return _firstName;
        }
        set
        {
            if (value != null && value != "")
            {
                _firstName = value;
            }
            else
            {
                throw new EmptyDataException("First Name is empty");
            }
        }
    }
    
    public string? LastName
    {
        get
        {
            return _lastName;
        }
        set
        {
            if (value != null && value != "")
            {
                _lastName = value;
            }
            else
            {
                throw new EmptyDataException("Last Name is empty");
            }
        }
    }

    public int Age {
        get
        {
            return _age;
        }
        set
        {
            if (value > 0 && value <= 150)
            {
                _age = value;
            }
            else
            {
                throw new AgeException("You introduced wrong age");
            }
        }
    }

    public override string ToString()
    {
        return $"Age: {this.Age}, First Name: {this.FirstName}, Last Name: {this.LastName}";
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) || this.ToString().Equals(obj?.ToString());
    }
}