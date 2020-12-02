using System;
using Zenject;

namespace Script.ControllersCore
{
    public class ControllerFactory : IControllerFactory
    {
        DiContainer _container;

        public ControllerFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : ControllerBase
        {
            return Create<T>(typeof(T));
        }

        public T Create<T>(Type type) where T : ControllerBase
        {
            return _container.Instantiate<T>();
        }
    }
}