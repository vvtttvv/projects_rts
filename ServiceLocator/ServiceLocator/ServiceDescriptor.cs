namespace ServiceLocator;

public class ServiceDescriptor
{
    public Type ImplementationType { get; }
    public ServiceLifeTime LifeTime { get; }
    public object? Implementation { get; set; }
    
    public ServiceDescriptor(Type implementationType, ServiceLifeTime lifeTime)
    {
        ImplementationType = implementationType;
        LifeTime = lifeTime;
    }
}
