using ServiceLocator.Mock;
namespace ServiceLocator.Tests;

public class Tests
{
    private ServiceLocator _slTest;

    public interface IMultipleConstructorHandler { };
    public class MultipleConstructorHandler
    {
        public MultipleConstructorHandler() { }
        public MultipleConstructorHandler(ICar car, IPassenger pas) { }
    }
    
    [SetUp]
    public void Setup()
    {
        _slTest = new ServiceLocator();
    }

    [Test]
    public void TestTransientInstances()
    {
        _slTest.Register<IPassenger, Passenger>(ServiceLifeTime.Transient);
        IPassenger passenger = _slTest.Get<IPassenger>();
        IPassenger passenger2 = _slTest.Get<IPassenger>();
        Assert.That(passenger, Is.Not.SameAs(passenger2));
    }
    
    [Test]
    public void TestSingletonInstances()
    {
        _slTest.Register<IPassenger, Passenger>(ServiceLifeTime.Singleton);
        IPassenger passenger = _slTest.Get<IPassenger>();
        IPassenger passenger2 = _slTest.Get<IPassenger>();
        Assert.That(passenger, Is.SameAs(passenger2));
    }
    
    [Test]
    public void TestServiceInService()
    {
        _slTest.Register<ICar, Car>(ServiceLifeTime.Singleton);
        _slTest.Register<IPassenger, Passenger>(ServiceLifeTime.Singleton);
        ICar car = _slTest.Get<ICar>();
        Assert.That(car.Passenger, Is.Not.Null);
    }
    
    [Test]
    public void TestRegisterTwoSameServices()
    {
        _slTest.Register<ICar, Car>(ServiceLifeTime.Singleton);
        Assert.Throws<Exception>(() => 
            _slTest.Register<ICar, Car>(ServiceLifeTime.Singleton)
        );
    }
    
    [Test]
    public void TestMultipleConstructors()
    {
        Assert.Throws<Exception>(() => 
            _slTest.Register<IMultipleConstructorHandler, MultipleConstructorHandler>(ServiceLifeTime.Transient)
        );
    }
    
    [Test]
    public void TestGetNotRegistered()
    {
        Assert.Throws<Exception>(() => 
            _slTest.Get<ICar>()
        );
    }
}