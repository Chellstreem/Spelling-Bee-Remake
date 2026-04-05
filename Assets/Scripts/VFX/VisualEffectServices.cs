using System;
using System.Collections.Generic;

namespace VFX
{
    public interface IVisualEffectService { }

    public class VisualEffectServices
    {
        private readonly Dictionary<Type, object> services = new();

        public void RegisterService<T>(T service) where T : IVisualEffectService
        {
            var type = typeof(T);

            if (!services.ContainsKey(type))
                services[type] = service;
        }

        public T GetService<T>() where T : IVisualEffectService
        {
            var type = typeof(T);

            if (services.TryGetValue(type, out var service))
                return (T)service;

            throw new Exception($"Service {type} not registered");
        }

        public void Unregister<T>() => services.Remove(typeof(T));
    }
}