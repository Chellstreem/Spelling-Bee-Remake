using System.Collections.Generic;
using System;
using System.Diagnostics;
using Sound;

public class GameContext
{
    private readonly Dictionary<Type, object> _services = new();

    public void Register<T>(T service)
    {
        if (service.Equals(typeof(AudioSourcePool)))
            UnityEngine.Debug.Log("sadad");

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
