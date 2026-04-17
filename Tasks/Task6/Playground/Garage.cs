namespace Task6;

using System.Collections;

public class Garage : IEnumerable
{
    private Car[] cars = new Car[4];

    public Garage()
    {
        cars[0] = new Car();
        cars[1] = new Car();
        cars[2] = new Car();
        cars[3] = new Car();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return cars.GetEnumerator();
    }
}