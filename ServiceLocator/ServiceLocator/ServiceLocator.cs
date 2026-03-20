namespace ServiceLocator;

public class ServiceLocator
{
    private Dictionary<Type, ServiceDescriptor>
        _dict = new();

    public bool Register<TInterface, TImplementation>(ServiceLifeTime lifetime)
    {
        if (!typeof(TImplementation).IsAssignableTo(typeof(TInterface)))
            throw new Exception($"{typeof(TImplementation).Name} does not implement {typeof(TInterface).Name}");
        if (_dict.ContainsKey(typeof(TInterface))) throw new Exception("Trying to register same service again!");
        if(typeof(TImplementation).GetConstructors().Length != 1) throw new Exception("Multiple constructors!");
        
        ServiceDescriptor descriptor = new ServiceDescriptor(typeof(TImplementation), lifetime);
        _dict.Add(typeof(TInterface), descriptor);
        return true;
    }
    
    public TInterface Get<TInterface>()
    {
        return (TInterface) GetService(typeof(TInterface));
    }

    private object CreateService(Type implementationType)
    {
        var constructor = implementationType.GetConstructors().First();
        var parameters = constructor.GetParameters()
            .Select(param => GetService(param.ParameterType))
            .ToArray();

        return constructor.Invoke(parameters);
    }

    private object GetService(Type type)
    {
        if (!_dict.ContainsKey(type)) throw new Exception($"Service {type.Name} is not registered.");
        
        var descriptor = _dict[type];
        
        if (descriptor.LifeTime == ServiceLifeTime.Singleton)
        {
            if (descriptor.Implementation != null)
            {
                return descriptor.Implementation;
            }

            var implementation = CreateService(descriptor.ImplementationType);
            descriptor.Implementation = implementation;
            return implementation;
        }

        return CreateService(descriptor.ImplementationType);
    }

}