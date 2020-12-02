using System.Threading.Tasks;
using Script.ControllersCore;
using Script.ControllersEvents;
using UnityEngine;

namespace Script.GameControllers
{
    public class GameRootController : RootController
    {
        private ControllerBase _gameLoopController;
        
        public GameRootController(IControllerFactory controllerFactory) : base(controllerFactory)
        {
            
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
            Debug.Log("Initialize root");
        }

        protected override async Task OnStartAsync()
        {
            Debug.Log("Start root");

            await RestartGame();
        }
        
        private async Task RestartGame()
        {
            await GameLoop();
            Debug.Log($"{_gameLoopController.GetType()} is {_gameLoopController.State}");
        }

        private async Task GameLoop()
        {
            _gameLoopController = await CreateAndRunAsync<GameLoopController>(CancellationToken);
        }

        protected override Task OnStopAsync()
        {
            return Task.CompletedTask;
        }

        protected override async void ThrowUnhandledEventAssert(IEvent e)
        {
            if (e is EndGameEvent)
            {
                await StopAndRemoveController(_gameLoopController);
                Debug.Log(_gameLoopController.State);
                await RestartGame();
            }
        }
    }
}