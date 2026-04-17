namespace Task3;

public class ObjectCounter
{
    // Реализовать тип, в котором подсчитывается количество созданных объектов. Сделать метод для получения этой информации. 
    // Сделать свойство для получения этой информации, но запретить внешнему коду возможность изменять значение свойства через присвоение. 
    private static int count = 0;

    public ObjectCounter() => count++;

    public static int GetInfo()
    {
        return count;
    }

    public static int Count
    {
        get
        {
            return count;
        }
    } 
}