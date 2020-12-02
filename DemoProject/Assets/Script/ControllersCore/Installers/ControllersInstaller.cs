using Script.ControllersCore;
using UnityEngine;
using Zenject;

public class ControllersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IControllerFactory>().To<ControllerFactory>().AsSingle();
    }

    public override async void Start()
    {
        base.Start();
    }
}