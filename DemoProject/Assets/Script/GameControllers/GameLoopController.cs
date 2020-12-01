using System;
using Script.BlocksMovement;
using Script.GameControllersInterfaces;
using UnityEngine;
using Zenject;

namespace Script.GameControllers
{
    public class GameLoopController : IGameLoopController, IInitializable, IDisposable
    {
        private readonly Board _board;
        private readonly BlocksSpawner _blocksSpawner;
        private readonly IUIWindow _endGameUIWindow;

        public GameLoopController(BlocksSpawner blocksSpawner, IUIWindow endGameUIWindow, Board board)
        {
            _blocksSpawner = blocksSpawner;
            _endGameUIWindow = endGameUIWindow;
            _board = board;
        }
        
        public void Initialize()
        {
            _endGameUIWindow.OnCloseButtonPressed += StartGame;
            _board.OnTopBorderReached += StopGame;
            _board.OnBlockPlaced += SpawnNewBlock;
        }

        public void StartGame()
        {
            _endGameUIWindow.HideWindow();
            _board.ClearBoard();
            _blocksSpawner.SpawnNewBlock();
        }

        public void StopGame()
        {
            _endGameUIWindow.ShowWindow();
        }

        private void SpawnNewBlock()
        {
            _blocksSpawner.SpawnNewBlock();
        }
        
        public void Dispose()
        {
            _endGameUIWindow.OnCloseButtonPressed -= StartGame;
            _board.OnTopBorderReached -= StopGame;
            _board.OnBlockPlaced -= SpawnNewBlock;
        }
    }
}