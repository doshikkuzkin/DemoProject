using System.Threading.Tasks;
using Script.BlocksMovement;
using Script.ControllersCore;
using Script.Installers;

namespace Script.GameControllers
{
    public class ScoreController : ControllerBase
    {
        // private IScoreView _scoreView;
        // private IScoreModel _scoreModel;
        private ILinesCleaner _linesCleaner;

        private IScoreProcessor _scoreProcessor;
        // private DifficultyLevelsConfig _difficultyLevels;
        // private BlocksSpeedSettings _blocksSpeedSettings;
        
        public ScoreController(IControllerFactory controllerFactory,
            /*IScoreView scoreView,
            IScoreModel scoreModel,*/
            IScoreProcessor scoreProcessor,
            ILinesCleaner board/*,
            DifficultyLevelsConfig difficultyLevels,
            BlocksSpeedSettings blocksSpeedSettings*/) 
            : base(controllerFactory)
        {
            // _scoreView = scoreView;
            // _scoreModel = scoreModel;
            _linesCleaner = board;
            _scoreProcessor = scoreProcessor;
            // _difficultyLevels = difficultyLevels;
            // _blocksSpeedSettings = blocksSpeedSettings;
        }

        protected override Task OnStartAsync()
        {
            _scoreProcessor.DropToInitialScore();
            _linesCleaner.OnLineCompleted += _scoreProcessor.UpdateScore;
            return Task.CompletedTask;
        }
        
        // private void DropToInitialDifficulty()
        // {
        //     _scoreModel.CurrentLinesCount = 0;
        //     _scoreModel.CurrentScore = 0;
        //     _scoreModel.CurrentLevelIndex = 0;
        //     _scoreView.DisplayScoreView(_scoreModel);
        //     _blocksSpeedSettings.currentSpeed = _difficultyLevels.levels[_scoreModel.CurrentLevelIndex].secondsBetweenBlockMove;
        // }
        //
        // private void UpdateScore()
        // {
        //     IncrementLines();
        //     IncrementScore();
        //     CheckNextLevelReached();
        //     _scoreView.DisplayScoreView(_scoreModel);
        // }
        //
        // private void IncrementLines()
        // {
        //     _scoreModel.CurrentLinesCount ++;
        // }
        //
        // private void IncrementScore()
        // {
        //     _scoreModel.CurrentScore += _difficultyLevels.levels[_scoreModel.CurrentLevelIndex].scorePointsForLine;
        // }
        //
        // private void CheckNextLevelReached()
        // {
        //     if (_scoreModel.CurrentLevelIndex < _difficultyLevels.levels.Length - 1)
        //     {
        //         if (_scoreModel.CurrentLinesCount >= _difficultyLevels.levels[_scoreModel.CurrentLevelIndex].linesToComplete)
        //         {
        //             _scoreModel.CurrentLevelIndex++;
        //             _blocksSpeedSettings.currentSpeed = _difficultyLevels.levels[_scoreModel.CurrentLevelIndex].secondsBetweenBlockMove;
        //         }
        //     }
        // }

        protected override Task OnStopAsync()
        {
            _linesCleaner.OnLineCompleted -= _scoreProcessor.UpdateScore;
            return Task.CompletedTask;
        }
    }
}