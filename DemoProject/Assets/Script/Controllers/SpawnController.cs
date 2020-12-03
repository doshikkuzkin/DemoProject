using System.Threading.Tasks;
using Script.BlocksMovement;
using Script.ControllersCore;
using Script.ControllersEvents;
using UnityEngine;

namespace Script.Controllers
{
    public class SpawnController : ControllerBase
    {
        private BlocksSpawner _blocksSpawner;
        private Board.Board _board;
        
        public SpawnController(IControllerFactory controllerFactory,
            BlocksSpawner blocksSpawner,
            Board.Board board)
            : base(controllerFactory)
        {
            _blocksSpawner = blocksSpawner;
            _board = board;
        }

        protected override Task OnStartAsync()
        {
            _board.ClearBoard();
            _board.OnBlockPlaced += _blocksSpawner.SpawnNewBlock;
            _board.OnTopBorderReached += OnBoardFilled;
            _blocksSpawner.SpawnNewBlock();
            return Task.CompletedTask;
        }

        protected override Task OnStopAsync()
        {
            Debug.Log("Stop Spawn Controller");
            _board.OnBlockPlaced -= _blocksSpawner.SpawnNewBlock;
            _board.OnTopBorderReached -= OnBoardFilled;
            return Task.CompletedTask;
        }

        private void OnBoardFilled()
        {
            DispatchBubblingEvent(new EndGameEvent());
        }
    }
}