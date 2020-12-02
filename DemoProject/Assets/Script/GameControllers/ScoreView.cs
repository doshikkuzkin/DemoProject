using System;
using Script.BlocksMovement;
using Script.GameControllersInterfaces;
using Script.Installers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Script.GameControllers
{
    public class ScoreView : IScoreController, IInitializable
    {
        public event Action<int> OnLevelUpdated;
        public event Action<int, int> OnScoreUpdated;

        private Board _board;
        private DifficultyLevels _difficultyLevels;
        private BlocksSpeedSettings _currentSpeedSettings;
        private int _currentLevelIndex;
        private int _linesCount;
        private int _playerScore;

        public ScoreView(Board board, DifficultyLevels difficultyLevels, BlocksSpeedSettings blocksSpeedSettings)
        {
            _board = board;
            _difficultyLevels = difficultyLevels;
            _currentSpeedSettings = blocksSpeedSettings;
        }
        
        public void Initialize()
        {
            _board.OnBoardClean += DropToInitialDifficulty;
            _board.OnLineCompleted += IncrementLines;
            _currentSpeedSettings.currentSpeed = _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove;
        }

        private void DropToInitialDifficulty()
        {
            _linesCount = 0;
            _playerScore = 0;
            OnScoreUpdated?.Invoke(_linesCount, _playerScore);
            _currentLevelIndex = 0;
            OnLevelUpdated?.Invoke(_currentLevelIndex + 1);
            _currentSpeedSettings.currentSpeed = _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove;
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
                    OnLevelUpdated?.Invoke(_currentLevelIndex + 1);
                    _currentSpeedSettings.currentSpeed = _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove;
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