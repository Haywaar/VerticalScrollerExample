using System;
using System.Collections.Generic;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    public class ServiceLocator
    {
        private ServiceLocator()
        {
        }

        /// <summary>
        /// Зарегистрированные сервисы
        /// </summary>
        private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();
        
        public static ServiceLocator Current { get; private set; }

        public static void Initialize()
        {
            Current = new ServiceLocator();
        }
        
        // Возвращает сервис нужного нам типа
        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)_services[key];
        }
        
        /// <summary>
        /// Регистрирует сервис в текущем сервис локаторе
        /// </summary>
        /// <typeparam name="T">Тип сервиса </typeparam>
        /// <param name="service">Экземпляр сервиса</param>
        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }

            _services.Add(key, service);
        }
        
        /// <summary>
        /// Убирает сервис из текущего сервис локатора
        /// </summary>
        /// <typeparam name="T">Тип сервиса.</typeparam>
        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            _services.Remove(key);
        }
    }
}