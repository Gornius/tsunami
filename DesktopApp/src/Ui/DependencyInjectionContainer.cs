using System;
using System.Collections.Generic;
using DesktopApp.Data;
using DesktopApp.Service;
using DesktopApp.Util;

namespace DesktopApp.Ui
{
    public class DependencyInjectionContainer
    {
        private readonly Dictionary<Type, Func<object>> _factories = new();

        public DependencyInjectionContainer()
        {
            Build();
        }

        private void Build()
        {
            _factories[typeof(IErrorHandler)] = () => new ConsoleErrorHandler();

            // Singletons
            var database = new Database(Get<IErrorHandler>());
            _factories[typeof(Database)] = () => database;
        }

        public T Get<T>()
        {
            var factory = _factories[typeof(T)];
            return (T) factory();
        }
    }
}