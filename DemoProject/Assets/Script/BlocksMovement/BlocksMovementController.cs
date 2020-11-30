using System;
using Script.GameControllers;
using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlocksMovementController : IInitializable
    {
        private Transform _spawnPoint;
        private BlocksSpawner _blocksSpawner;
        private Board _board;
        private ScoreController _scoreController;
        private GameLoopController _gameLoopController;
        
        private float _secondsPassedAfterMove;
        private float _secondsBetweenMove;
        private float _normalSecondsBetweenMove;

        public BlocksMovementController(Transform spawnPoint, BlocksSpawner blocksSpawner, 
            Board board, GameLoopController gameLoopController, ScoreController scoreController)
        {
            _spawnPoint = spawnPoint;
            _blocksSpawner = blocksSpawner;
            _board = board;
            _gameLoopController = gameLoopController;

            _scoreController = scoreController;

            _normalSecondsBetweenMove = _scoreController.GetInitialDifficulty();
            _secondsBetweenMove = _normalSecondsBetweenMove;
        }
        
        public void Initialize()
        {
            _scoreController.OnLevelChanged += OnDifficultyLevelChanged;
        }

        private void OnDifficultyLevelChanged(int level, float secondsBetweenMove)
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
                    
                    foreach (Transform childTransform in blockFacade.Transform)
                    {
                        if (childTransform.position.y >= _spawnPoint.position.y)
                        {
                            _gameLoopController.StopGame();
                            blockFacade.IsDisabled = true;
                        }
                    }

                    var parentTransform = blockFacade.Transform.parent;
                    blockFacade.Transform.parent = null;
                    _board.DestroyGameObject(parentTransform);
                    
                    blockFacade.Transform.DetachChildren();
                    _board.DestroyGameObject(blockFacade.Transform);

                    _board.CheckForFullLines();
                    
                    if (blockFacade.IsDisabled) return;
                    
                    _blocksSpawner.SpawnNewBlock();
                }
            }
        }
    }
}