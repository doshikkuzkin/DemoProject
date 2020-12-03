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
        private ControllerBase _movementController;
        private ControllerBase _spawnController;
        private ControllerBase _scoreController;

        public GameLoopController(IControllerFactory controllerFactory, IUIWindow endGameView) : base(controllerFactory)
        {
            _endGameView = endGameView;
        }

        protected override void OnInitialize()
        {
            Debug.Log("Initialize game loop");
        }

        protected override Task OnStartAsync()
        {
            Debug.Log("Start game loop");
            _endGameView.ShowWindow();
            _endGameView.OnCloseButtonPressed += StartGame;
            return Task.CompletedTask;
        }

        private async void StartGame()
        {
            AudioPlayer.Instance.PlaySound(SoundType.StartGame);
            _endGameView.HideWindow();
            _movementController = await CreateAndRunAsync<MovementController>(CancellationToken);
            Debug.Log($"{_movementController.GetType()} is {_movementController.State}");
            _spawnController = await CreateAndRunAsync<SpawnController>(CancellationToken);
            Debug.Log($"{_spawnController.GetType()} is {_spawnController.State}");
            _scoreController = await CreateAndRunAsync<ScoreController>(CancellationToken);
            Debug.Log($"{_scoreController.GetType()} is {_scoreController.State}");
        }

        protected override Task OnStopAsync()
        {
            _endGameView.OnCloseButtonPressed -= StartGame;
            return Task.CompletedTask;
        }
        
        
    }
}