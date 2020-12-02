using System;

namespace Script.ControllersCore
{
    public interface IControllerFactory
    {
        T Create<T>() where T : ControllerBase;
        T Create<T>(Type type) where T : ControllerBase;
    }
}