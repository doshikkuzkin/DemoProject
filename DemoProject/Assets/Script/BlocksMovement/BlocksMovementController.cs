using System;
using Script.GameControllers;
using Script.GameControllersInterfaces;
using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlocksMovementController : IInitializable
    {
        private Board _board;
        private IScoreController _scoreController;
        private IGameLoopController _gameLoopController;
        
        private float _secondsPassedAfterMove;
        private float _secondsBetweenMove;
        private float _normalSecondsBetweenMove;

        public BlocksMovementController(Board board, IGameLoopController gameLoopController, IScoreController scoreController)
        {
            _board = board;
            _gameLoopController = gameLoopController;

            _scoreController = scoreController;

            _normalSecondsBetweenMove = _scoreController.GetInitialDifficulty();
            _secondsBetweenMove = _normalSecondsBetweenMove;
        }
        
        public void Initialize()
        {
            _scoreController.OnLevelUpdated += OnDifficultyLevelUpdated;
        }

        private void OnDifficultyLevelUpdated(int level, float secondsBetweenMove)
        {
            _normalSecondsBetweenMove = secondsBetweenMove;
        }

        public void Move(BlockFacade blockFacade)
        {
            HandleUserInput(blockFacade);
            
            MoveDownConstant(blockFacade);
        }

        private void HandleUserInput(BlockFacade blockFacade)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveByControls(blockFacade, Vector3.right);
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveByControls(blockFacade, Vector3.left);
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateByControls(blockFacade);
            }
            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                ChangeMoveRate(_normalSecondsBetweenMove / 15);
            }
            else
            {
                ChangeMoveRate(_normalSecondsBetweenMove);
            }
        }

        private void MoveByControls(BlockFacade blockFacade, Vector3 movement)
        {
            blockFacade.Transform.position += movement;
            if (!_board.CheckMovementIsValid(blockFacade.Transform))
            {
                blockFacade.Transform.position -= movement;
            }
        }

        private void RotateByControls(BlockFacade blockFacade)
        {
            blockFacade.Transform.RotateAround(blockFacade.Transform.TransformPoint(blockFacade.RotationPoint), Vector3.forward, 90);
            if (!_board.CheckMovementIsValid(blockFacade.Transform))
            {
                blockFacade.Transform.RotateAround(blockFacade.Transform.TransformPoint(blockFacade.RotationPoint), Vector3.forward, -90);
            }
        }

        private void ChangeMoveRate(float secondsBetweenMove)
        {
            _secondsBetweenMove = secondsBetweenMove;
        }
        
        private void MoveDownConstant(BlockFacade blockFacade)
        {
            _secondsPassedAfterMove += Time.deltaTime;
            if (_secondsPassedAfterMove >= _secondsBetweenMove)
            {
                blockFacade.Transform.position += Vector3.down;
                _secondsPassedAfterMove = 0;
                if (!_board.CheckMovementIsValid(blockFacade.Transform))
                {
                    blockFacade.Transform.position -= Vector3.down;
                    _board.AddToGrid(blockFacade.Transform);
                }
            }
        }
    }
}