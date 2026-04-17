namespace Task4;

public class Person
{
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

    public override string ToString()
    {
        return $"Age: {this.Age}, First Name: {this.FirstName}, Last Name: {this.LastName}";
    }

    public override bool Equals(object? obj)
    {
        return base.Equals(obj) || this.ToString().Equals(obj?.ToString());
    }
}