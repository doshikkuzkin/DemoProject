using System;
using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class Board : MonoBehaviour
    {
        private Transform[,] _grid;
        private BoardSettings _boardSettings;

        [Inject]
        public void Construct(BoardSettings boardSettings)
        {
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
        }

        public void DestroyGameObject(Transform gameObjectTransform)
        {
            Destroy(gameObjectTransform.gameObject);
        }

        public void CheckForFullLines()
        {
            for (int i = _boardSettings.boardTopBoundary; i >= 0; i--)
            {
                if (HasFullLine(i))
                {
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
                Destroy(_grid[rowIndex, lineIndex].gameObject);
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
        
        [Serializable]
        public class BoardSettings
        {
            public int boardRightBoundary;
            public int boardLeftBoundary;
            public int boardBottomBoundary;
            public int boardTopBoundary;
        }
    }
}