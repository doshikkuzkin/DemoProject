using System;
using Script.BlocksMovement;
using Script.GameControllersInterfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Script.GameControllers
{
    public class ScoreController : IScoreController, IInitializable
    {
        public event Action<int, float> OnLevelUpdated;
        public event Action<int, int> OnScoreUpdated;

        private Board _board;
        private DifficultyLevels _difficultyLevels;
        private int _currentLevelIndex;
        private int _linesCount;
        private int _playerScore;

        public ScoreController(Board board, DifficultyLevels difficultyLevels)
        {
            _board = board;
            _difficultyLevels = difficultyLevels;
        }
        
        public void Initialize()
        {
            _board.OnBoardClean += DropToInitialDifficulty;
            _board.OnLineCompleted += IncrementLines;
        }

        private void DropToInitialDifficulty()
        {
            _linesCount = 0;
            _playerScore = 0;
            OnScoreUpdated?.Invoke(_linesCount, _playerScore);
            _currentLevelIndex = 0;
            OnLevelUpdated?.Invoke(_currentLevelIndex + 1, _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove);
        }

        public float GetInitialDifficulty()
        {
            return _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove;
        }

        public void IncrementLines()
        {
            _linesCount ++;
            IncrementScore();
            CheckNextLevelReached();
        }

        private void IncrementScore()
        {
            _playerScore += _difficultyLevels.levels[_currentLevelIndex].scorePointsForLine;
            OnScoreUpdated?.Invoke(_linesCount, _playerScore);
        }

        private void CheckNextLevelReached()
        {
            if (_currentLevelIndex < _difficultyLevels.levels.Length - 1)
            {
                if (_linesCount >= _difficultyLevels.levels[_currentLevelIndex].linesToComplete)
                {
                    _currentLevelIndex++;
                    OnLevelUpdated?.Invoke(_currentLevelIndex + 1, _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove);
                }
            }
        }
        
        [Serializable]
        public class DifficultyLevels
        {
            public DifficultyLevel[] levels;
        }

        [Serializable]
        public class DifficultyLevel
        {
            public int linesToComplete;
            public float secondsBetweenBlockMove;
            public int scorePointsForLine;
        }
    }
}