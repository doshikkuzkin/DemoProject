using Script.Installers;

namespace Script.GameControllers
{
    public class ScoreProcessor : IScoreProcessor
    {
        public IScoreView ScoreView { get; }
        public IScoreModel ScoreModel { get; }
        
        private DifficultyLevelsConfig _difficultyLevels;
        private BlocksSpeedSettings _blocksSpeedSettings;

        public ScoreProcessor(DifficultyLevelsConfig difficultyLevels, BlocksSpeedSettings blocksSpeedSettings, IScoreView scoreView, IScoreModel scoreModel)
        {
            _difficultyLevels = difficultyLevels;
            _blocksSpeedSettings = blocksSpeedSettings;
            ScoreView = scoreView;
            ScoreModel = scoreModel;
        }

        public void DropToInitialScore()
        {
            ScoreModel.CurrentLinesCount = 0;
            ScoreModel.CurrentScore = 0;
            ScoreModel.CurrentLevelIndex = 0;
            ScoreView.DisplayScoreView(ScoreModel);
            _blocksSpeedSettings.currentSpeed = _difficultyLevels.levels[ScoreModel.CurrentLevelIndex].secondsBetweenBlockMove;
        }

        public void UpdateScore()
        {
            IncrementLines();
            IncrementScore();
            IncrementLevel();
            ScoreView.DisplayScoreView(ScoreModel);
        }

        public void IncrementLines()
        {
            ScoreModel.CurrentLinesCount ++;
        }

        public void IncrementScore()
        {
            ScoreModel.CurrentScore += _difficultyLevels.levels[ScoreModel.CurrentLevelIndex].scorePointsForLine;
        }

        public void IncrementLevel()
        {
            if (CheckNextLevelReached())
            {
                ScoreModel.CurrentLevelIndex++;
                _blocksSpeedSettings.currentSpeed = _difficultyLevels.levels[ScoreModel.CurrentLevelIndex].secondsBetweenBlockMove;
            }
        }

        public bool CheckNextLevelReached()
        {
            if (ScoreModel.CurrentLevelIndex < _difficultyLevels.levels.Length - 1)
            {
                return ScoreModel.CurrentLinesCount >=
                       _difficultyLevels.levels[ScoreModel.CurrentLevelIndex].linesToComplete;
            }

            return false;
        }
    }
}