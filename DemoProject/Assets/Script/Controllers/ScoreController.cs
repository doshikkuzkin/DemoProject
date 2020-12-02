using System.Threading.Tasks;
using Script.BlocksMovement;
using Script.ControllersCore;
using Script.Installers;

namespace Script.GameControllers
{
    public class ScoreController : ControllerBase
    {
        private ScoreView _scoreView;
        private Board _board;
        private DifficultyLevelsConfig _difficultyLevels;
        private BlocksSpeedSettings _blocksSpeedSettings;
        
        private int _currentLevelIndex;
        private int _linesCount;
        private int _playerScore;
        
        public ScoreController(IControllerFactory controllerFactory,
            ScoreView scoreView,
            Board board,
            DifficultyLevelsConfig difficultyLevels,
            BlocksSpeedSettings blocksSpeedSettings) 
            : base(controllerFactory)
        {
            _scoreView = scoreView;
            _board = board;
            _difficultyLevels = difficultyLevels;
            _blocksSpeedSettings = blocksSpeedSettings;
        }

        protected override Task OnStartAsync()
        {
            DropToInitialDifficulty();
            _board.OnLineCompleted += IncrementLines;
            return Task.CompletedTask;
        }
        
        private void DropToInitialDifficulty()
        {
            _linesCount = 0;
            _playerScore = 0;
            _scoreView.UpdateScoreText(_linesCount, _playerScore);
            _currentLevelIndex = 0;
            _scoreView.UpdateLevelText(_currentLevelIndex + 1);
            _blocksSpeedSettings.currentSpeed = _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove;
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
            _scoreView.UpdateScoreText(_linesCount, _playerScore);
        }

        private void CheckNextLevelReached()
        {
            if (_currentLevelIndex < _difficultyLevels.levels.Length - 1)
            {
                if (_linesCount >= _difficultyLevels.levels[_currentLevelIndex].linesToComplete)
                {
                    _currentLevelIndex++;
                    _scoreView.UpdateLevelText(_currentLevelIndex + 1);
                    _blocksSpeedSettings.currentSpeed = _difficultyLevels.levels[_currentLevelIndex].secondsBetweenBlockMove;
                }
            }
        }

        protected override Task OnStopAsync()
        {
            _board.OnLineCompleted -= IncrementLines;
            return Task.CompletedTask;
        }
    }
}