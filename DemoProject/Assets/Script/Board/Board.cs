using System;
using Script.Audio;
using Script.GameControllers;
using Script.GameControllersInterfaces;
using Script.Installers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Script.BlocksMovement
{
    public class Board : ILinesCleaner
    {
        public event Action OnLineCompleted;
        public event Action OnTopBorderReached;
        public event Action OnBlockPlaced;
        
        private Transform _spawnPoint;
        private Transform[,] _grid;
        private BoardSettings _boardSettings;

        public Board(Transform spawnPoint, BoardSettings boardSettings)
        {
            _spawnPoint = spawnPoint;
            _boardSettings = boardSettings;
            
            _grid = new Transform[_boardSettings.boardRightBoundary + 1,_boardSettings.boardTopBoundary + 1];
        }

        public bool CheckMovementIsValid(Transform blockTransform)
        {
            foreach (Transform block in blockTransform)
            {
                var position = block.position;
                
                var roundedX = Mathf.RoundToInt(position.x);
                var roundedY = Mathf.RoundToInt(position.y);

                if (roundedX > _boardSettings.boardRightBoundary ||
                    roundedX < _boardSettings.boardLeftBoundary ||
                    roundedY < _boardSettings.boardBottomBoundary || 
                    roundedY > _boardSettings.boardTopBoundary)
                {
                    return false;
                }

                if (_grid[roundedX, roundedY] != null)
                {
                    return false;
                }
            }
            return true;
        }

        public void AddToGrid(Transform blockTransform)
        {
            foreach (Transform block in blockTransform)
            {
                var position = block.position;
                
                var roundedX = Mathf.RoundToInt(position.x);
                var roundedY = Mathf.RoundToInt(position.y);

                _grid[roundedX, roundedY] = block;
            }
            
            CheckIfTopBorderReached(blockTransform);
        }

        private void CheckIfTopBorderReached(Transform blockTransform)
        {
            bool borderIsReached = false;
            
            foreach (Transform childTransform in blockTransform)
            {
                if (childTransform.position.y >= _spawnPoint.position.y)
                {
                    borderIsReached = true;
                }
            }
            
            var parentTransform = blockTransform.parent;
            blockTransform.parent = null;
            DestroyGameObject(parentTransform);
                    
            blockTransform.DetachChildren();
            DestroyGameObject(blockTransform);

            if (borderIsReached)
            {
                AudioPlayer.Instance.PlaySound(SoundType.EndGame);
                OnTopBorderReached?.Invoke();
            }
            else
            {
                CheckForFullLines();
                OnBlockPlaced?.Invoke();
            }
        }

        private void DestroyGameObject(Transform gameObjectTransform)
        {
            Object.Destroy(gameObjectTransform.gameObject);
        }

        private void CheckForFullLines()
        {
            for (int i = _boardSettings.boardTopBoundary; i >= 0; i--)
            {
                if (HasFullLine(i))
                {
                    OnLineCompleted?.Invoke();
                    DeleteLine(i);
                    MoveRowDown(i);
                }
                
            }
        }

        private bool HasFullLine(int lineIndex)
        {
            for (int rowIndex = 0; rowIndex <= _boardSettings.boardRightBoundary; rowIndex++)
            {
                if (_grid[rowIndex, lineIndex] == null) return false;
            }
            return true;
        }

        private void DeleteLine(int lineIndex)
        {
            for (int rowIndex = 0; rowIndex <= _boardSettings.boardRightBoundary; rowIndex++)
            {
                Object.Destroy(_grid[rowIndex, lineIndex].gameObject);
                _grid[rowIndex, lineIndex] = null;
            }
        }

        private void MoveRowDown(int lineIndex)
        {
            for (int currentLineIndex = lineIndex; currentLineIndex <= _boardSettings.boardTopBoundary; currentLineIndex++)
            {
                for (int rowIndex = 0; rowIndex <= _boardSettings.boardRightBoundary; rowIndex++)
                {
                    if (_grid[rowIndex, currentLineIndex] != null)
                    {
                        _grid[rowIndex, currentLineIndex - 1] = _grid[rowIndex, currentLineIndex];
                        _grid[rowIndex, currentLineIndex] = null;
                        _grid[rowIndex, currentLineIndex - 1].transform.position += Vector3.down;
                    }
                }
            }
        }

        public void ClearBoard()
        {
            for (int i = 0; i <= _boardSettings.boardTopBoundary; i++)
            {
                for (int j = 0; j <= _boardSettings.boardRightBoundary; j++)
                {
                    if (_grid[j, i] != null)
                    {
                        DestroyGameObject(_grid[j, i]);
                        _grid[j, i] = null;
                    }
                }
            }
        }
    }
}