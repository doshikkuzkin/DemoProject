using Script.BlocksMovement;
using UnityEngine;
using Zenject;

namespace Script.GameControllers
{
    public class GameLoopController : IInitializable
    {
        private BlocksMovementController _blocksMovementController;
        private Board _board;
        
        public GameLoopController(BlocksMovementController blocksMovementController, Board board)
        {
            _blocksMovementController = blocksMovementController;
            _board = board;
        }
        
        public void Initialize()
        {
            _blocksMovementController.OnGameOver += EndGame;
        }

        private void EndGame()
        {
            Debug.Log("Show UI Window");
        }

        private void RestartGame()
        {
            _board.ClearBoard();
        }
    }
}