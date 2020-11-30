using System;
using Script.BlocksMovement;
using UnityEngine;
using Zenject;

namespace Script.GameControllers
{
    public class GameLoopController : IInitializable, IDisposable
    {
        private Board _board;
        private BlocksSpawner _blocksSpawner;
        private EndGameUIController _endGameUIController;

        private bool _gameIsRunning;

        public bool GameIsRunning => _gameIsRunning;

        public GameLoopController(BlocksSpawner blocksSpawner, EndGameUIController endGameUIController, Board board)
        {
            _blocksSpawner = blocksSpawner;
            _endGameUIController = endGameUIController;
            _board = board;
        }
        
        public void Initialize()
        {
            _endGameUIController.OnPlayAgainButtonPressed += StartGame;
        }

        private void StartGame()
        {
            _board.ClearBoard();
            _blocksSpawner.SpawnNewBlock();
        }

        public void StopGame()
        {
            _endGameUIController.ShowUI();
        }
        
        public void Dispose()
        {
            _endGameUIController.OnPlayAgainButtonPressed -= StartGame;
        }
    }
}