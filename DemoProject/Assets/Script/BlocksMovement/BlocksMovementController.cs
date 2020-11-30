using System;
using Script.GameControllers;
using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlocksMovementController : ITickable
    {
        private Transform _spawnPoint;
        private BlockFacade _blockFacade;
        private BlocksSpawner _blocksSpawner;
        private Board _board;
        private GameLoopController _gameLoopController;
        
        private float _secondsPassedAfterMove;
        private float _secondsBetweenMove;
        private float _normalSecondsBetweenMove = 1f;

        public BlocksMovementController(Transform spawnPoint, BlockFacade blockFacade, BlocksSpawner blocksSpawner, 
            Board board, GameLoopController gameLoopController)
        {
            _spawnPoint = spawnPoint;
            _blockFacade = blockFacade;
            _blocksSpawner = blocksSpawner;
            _board = board;
            _gameLoopController = gameLoopController;
            
            _secondsBetweenMove = _normalSecondsBetweenMove;
            
            Debug.Log("Constructor");
        }

        public void Tick()
        {
            if (_blockFacade == null) return;
            if (_blockFacade.IsDisabled) return;

            HandleUserInput();
            
            MoveDownConstant();
        }

        private void HandleUserInput()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveByControls(Vector3.right);
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveByControls(Vector3.left);
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateByControls();
            }
            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                ChangeMoveRate(_normalSecondsBetweenMove / 10);
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                ChangeMoveRate(_normalSecondsBetweenMove);
            }
        }

        private void MoveByControls(Vector3 movement)
        {
            _blockFacade.Transform.position += movement;
            if (!_board.CheckMovementIsValid(_blockFacade.Transform))
            {
                _blockFacade.Transform.position -= movement;
            }
        }

        private void RotateByControls()
        {
            _blockFacade.Transform.RotateAround(_blockFacade.Transform.TransformPoint(_blockFacade.RotationPoint), Vector3.forward, 90);
            if (!_board.CheckMovementIsValid(_blockFacade.Transform))
            {
                _blockFacade.Transform.RotateAround(_blockFacade.Transform.TransformPoint(_blockFacade.RotationPoint), Vector3.forward, -90);
            }
        }

        private void ChangeMoveRate(float secondsBetweenMove)
        {
            _secondsBetweenMove = secondsBetweenMove;
        }
        
        private void MoveDownConstant()
        {
            _secondsPassedAfterMove += Time.deltaTime;
            if (_secondsPassedAfterMove >= _secondsBetweenMove)
            {
                _blockFacade.Transform.position += Vector3.down;
                _secondsPassedAfterMove = 0;
                if (!_board.CheckMovementIsValid(_blockFacade.Transform))
                {
                    _blockFacade.Transform.position -= Vector3.down;
                    _board.AddToGrid(_blockFacade.Transform);
                    
                    foreach (Transform childTransform in _blockFacade.Transform)
                    {
                        if (childTransform.position.y >= _spawnPoint.position.y)
                        {
                            _gameLoopController.StopGame();
                            _blockFacade.IsDisabled = true;
                        }
                    }

                    var parentTransform = _blockFacade.Transform.parent;
                    _blockFacade.Transform.parent = null;
                    _board.DestroyGameObject(parentTransform);
                    
                    _blockFacade.Transform.DetachChildren();
                    _board.DestroyGameObject(_blockFacade.Transform);

                    _board.CheckForFullLines();
                    
                    if (_blockFacade.IsDisabled) return;
                    
                    _blocksSpawner.SpawnNewBlock();
                }
            }
        }
    }
}