//
// class Program
// {
//     static void Main()
//     {  
//         Car cl = new Car ("SlugBug", 100, 10); 
//         cl.AboutToBlow += (sender, e) => { Console.WriteLine(e.msg);};
//         cl.Exploded += (sender, e) => { Console.WriteLine(e.msg); }; 
//         Console.WriteLine("\n***** Speeding up *****");
//         for (int i = 0; i < 6; i++)
//             cl.Accelerate(20); 
//     }
//
//     public static void OnCarEngineEvent(object sender, CarEventArgs e)
//     { 
//         Console.WriteLine ("{0} says: {1}", sender, e.msg);
//     }
//     
//     public static void OnCarEngineEvent2(object sender, CarEventArgs e)
//     {
//         Console.WriteLine("=> {0}", e.msg.ToUpper());
//     }
// }
//
//
// public delegate int BinaryOp(int x, int y) ;
// public class SimpleMath
// {
//     public int Add (int x, int y) => x + y;
//     public int Subtract (int x, int y) => x - y;
// }
//
// public class Car
// {
//     // Данные состояния,
//     public int CurrentSpeed { get; set; }
//     public int MaxSpeed { get; set; } = 100;
//     public string PetName { get; set; }
//     // Исправен ли автомобиль?
//     private bool _carIsDead;
//     // Конструкторы класса,
//     public Car () {}
//     public Car(string name, int maxSp, int currSp)
//     {
//         CurrentSpeed = currSp;
//         MaxSpeed = maxSp;
//         PetName = name;
//     }
//     public delegate void CarEngineHandler(object sender, CarEventArgs e);
//     public event CarEngineHandler Exploded;
//     public event CarEngineHandler AboutToBlow;
//     
//     public void Accelerate(int delta)
//     {
//         // Если этот автомобиль сломан, то отправить сообщение об этом,
//         if (_carIsDead)
//         {
//             Exploded?.Invoke(this, new CarEventArgs("Sorry, this car is dead..."));
//         }
//         else
//         {
//             CurrentSpeed += delta;
//             // Автомобиль почти сломан?
//             if (10 == (MaxSpeed - CurrentSpeed) && AboutToBlow != null)
//             {
//                 AboutToBlow(this, new CarEventArgs("Careful buddy! Gonna blow!"));
//             }
//             if (CurrentSpeed >= MaxSpeed)
//                 _carIsDead = true;
//             else
//                 Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
//         }
//     }
//     
// }
//
// public class CarEventArgs : EventArgs
// {
//     public readonly string msg;
//     public CarEventArgs(string message)
//     {
//         msg = message;
//     }
// }