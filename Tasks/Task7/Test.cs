namespace Task7;

public class Test
{
    public delegate void CarEngineHandler(string msg);
    
    public event CarEngineHandler ListHandler1;
    public CarEngineHandler ListHandler2;
}