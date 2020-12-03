using System.Threading.Tasks;
using Script.Audio;
using Script.Configs;
using Script.Controllers.Score;
using Script.ControllersCore;
using Script.Views;
using UnityEngine;

namespace Script.Controllers
{
    public class GameLoopController : ControllerBase
    {
        private readonly IUIWindow _endGameView;
        private readonly IAudioPlayer _audioPlayer;
        private ControllerBase _movementController;
        private ControllerBase _spawnController;
        private ControllerBase _scoreController;

        public GameLoopController(IControllerFactory controllerFactory, IUIWindow endGameView, IAudioPlayer audioPlayer) : base(controllerFactory)
        {
            _endGameView = endGameView;
            _audioPlayer = audioPlayer;
        }

        protected override Task OnStartAsync()
        {
            _endGameView.ShowWindow();
            _endGameView.OnCloseButtonPressed += StartGame;
            return Task.CompletedTask;
        }

        private async void StartGame()
        {
            _audioPlayer.PlaySound(SoundType.StartGame);
            _endGameView.HideWindow();
            _movementController = await CreateAndRunAsync<MovementController>(CancellationToken);
            _spawnController = await CreateAndRunAsync<SpawnController>(CancellationToken);
            _scoreController = await CreateAndRunAsync<ScoreController>(CancellationToken);
        }

        protected override Task OnStopAsync()
        {
            _endGameView.OnCloseButtonPressed -= StartGame;
            return Task.CompletedTask;
        }
        
        
    }
}