using System.Collections.Generic;
using System;

public interface IService { }

public class GameServices
{
    private readonly Dictionary<Type, object> _services = new();

    public void Register<T>(T service) where T : IService
    {
        var type = typeof(T);

        if (_services.ContainsKey(type))
            throw new Exception($"Service {type} already registered");

        _services[type] = service;
    }

    public T Get<T>()
    {
        var type = typeof(T);

        if (_services.TryGetValue(type, out var service))
            return (T)service;

        throw new Exception($"Service {type} not found");
    }
}
