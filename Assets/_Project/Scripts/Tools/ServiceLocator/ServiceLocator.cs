using System;
using System.Collections.Generic;

namespace _Project.Scripts.Tools.ServiceLocator
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> _services = new();
        public static T GetService<T>() where T : IService
        {
            return (T) _services[typeof(T)];
        }
        
        public static void RegisterService<T>(T service) where T : IService
        {
            _services.Add(typeof(T), service);
        }
        
        public static void UnregisterService<T>() where T : IService
        {
            _services.Remove(typeof(T));
        }
    }
}