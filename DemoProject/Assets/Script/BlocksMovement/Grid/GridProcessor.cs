using System;
using Script.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Script.BlocksMovement.Grid
{
    public class GridProcessor : ILinesCleaner, IGridProcessor
    {
        private bool _isTopBoarderReached;
        public IGridModel GridModel { get; set; }
        public event Action OnLineCompleted;
        public event Action OnTopBorderReached;
        public event Action OnBlockPlaced;
        
        private BoardSettings _boardSettings;

        public GridProcessor(BoardSettings boardSettings, IGridModel gridModel)
        {
            _boardSettings = boardSettings;
            GridModel = gridModel;
            GridModel.Grid = new Transform[_boardSettings.boardRightBoundary + 1, _boardSettings.boardTopBoundary + 1];
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

                if (GridModel.Grid[roundedX, roundedY] != null)
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

                GridModel.Grid[roundedX, roundedY] = block;
            }
        }

        public bool CheckIfTopBorderReached(Transform blockTransform)
        {
            _isTopBoarderReached = false;

            foreach (Transform childTransform in blockTransform)
            {
                if (childTransform.position.y >= GridModel.SpawnPoint.position.y)
                {
                    _isTopBoarderReached = true;
                    break;
                    
                }
            }

            return _isTopBoarderReached;
        }

        public void DetachChildren(Transform blockTransform)
        {
            var parentTransform = blockTransform.parent;
            if (parentTransform != null)
            {
                blockTransform.parent = null;
                DestroyGameObject(parentTransform);
            }

            blockTransform.DetachChildren();
            DestroyGameObject(blockTransform);

            if (_isTopBoarderReached)
            {
                OnTopBorderReached?.Invoke();
                return;
            }
            OnBlockPlaced?.Invoke();
        }

        private void DestroyGameObject(Transform gameObjectTransform)
        {
            Object.Destroy(gameObjectTransform.gameObject);
        }

        public void CheckForFullLines()
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

        public bool HasFullLine(int lineIndex)
        {
            for (int rowIndex = 0; rowIndex <= _boardSettings.boardRightBoundary; rowIndex++)
            {
                if (GridModel.Grid[rowIndex, lineIndex] == null) return false;
            }
            return true;
        }

        private void DeleteLine(int lineIndex)
        {
            for (int rowIndex = 0; rowIndex <= _boardSettings.boardRightBoundary; rowIndex++)
            {
                if (GridModel.Grid[rowIndex, lineIndex] != null)
                {
                    Object.Destroy(GridModel.Grid[rowIndex, lineIndex].gameObject);
                }
                GridModel.Grid[rowIndex, lineIndex] = null;
            }
        }

        private void MoveRowDown(int lineIndex)
        {
            for (int currentLineIndex = lineIndex; currentLineIndex <= _boardSettings.boardTopBoundary; currentLineIndex++)
            {
                for (int rowIndex = 0; rowIndex <= _boardSettings.boardRightBoundary; rowIndex++)
                {
                    if (GridModel.Grid[rowIndex, currentLineIndex] != null)
                    {
                        GridModel.Grid[rowIndex, currentLineIndex - 1] = GridModel.Grid[rowIndex, currentLineIndex];
                        GridModel.Grid[rowIndex, currentLineIndex] = null;
                        GridModel.Grid[rowIndex, currentLineIndex - 1].transform.position += Vector3.down;
                    }
                }
            }
        }

        public void ClearGrid()
        {
            for (int i = 0; i <= _boardSettings.boardTopBoundary; i++)
            {
                for (int j = 0; j <= _boardSettings.boardRightBoundary; j++)
                {
                    if (GridModel.Grid[j, i] != null)
                    {
                        DestroyGameObject(GridModel.Grid[j, i]);
                        GridModel.Grid[j, i] = null;
                    }
                }
            }
        }
    }
}