using System.Threading;
using Script.Controllers;
using Script.Controllers.Score;
using Zenject;

namespace Script.ControllersCore.Installers
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IControllerFactory>().To<ControllerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameRootController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopController>().AsSingle();
            Container.BindInterfacesAndSelfTo<MovementController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle();
        }

        public override async void Start()
        {
            base.Start();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var root = Container.Resolve<GameRootController>();
            root.Initialize(null, cancellationTokenSource.Token);
            await root.StartAsync();
        }
    }
}