using System;
using Script.BlocksMovement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.GameControllers
{
    public class ScoreController
    {
        public event Action<float> OnLevelChanged;

        private Board _board;
        private DifficultyLevels _difficultyLevels;
        private int _currentLevelIndex;
        private int _linesCount;
        private int _playerScore;

        public ScoreController(Board board)
        {
            _board = board;
        }

        public void IncrementLines()
        {
            _linesCount ++;
            IncrementScore();
        }

        private void IncrementScore()
        {
            _playerScore += _difficultyLevels.levels[_currentLevelIndex].scorePointsForLine;
        }

        private void CheckNextLevelReached()
        {
            if (_currentLevelIndex < _difficultyLevels.levels.Length - 1)
            {
                if (_linesCount >= _difficultyLevels.levels[_currentLevelIndex + 1].linesToReach)
                {
                    _currentLevelIndex++;
                    OnLevelChanged?.Invoke(_difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove);
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
            public int linesToReach;
            public float secondsBetweenBlockMove;
            public int scorePointsForLine;
        }
    }
}