using System.Threading.Tasks;
using Script.Audio;
using Script.ControllersCore;
using Script.ControllersEvents;

namespace Script.Controllers
{
    public class GameRootController : RootController
    {
        private ControllerBase _gameLoopController;
        private IAudioPlayer _audioPlayer;
        
        public GameRootController(IControllerFactory controllerFactory, IAudioPlayer audioPlayer) : base(controllerFactory)
        {
            _audioPlayer = audioPlayer;
        }

        protected override async Task OnStartAsync()
        {
            _audioPlayer.PlayBackgroundMusic();
            await RestartGame();
        }
        
        private async Task RestartGame()
        {
            await GameLoop();
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
                await RestartGame();
            }
        }
    }
}