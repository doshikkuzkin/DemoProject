using Script.Installers;

namespace Script.GameControllers
{
    public class ScoreModel : IScoreModel
    {
        public int CurrentScore { get; set; }
        public int CurrentLinesCount { get; set; }
        public int CurrentLevelIndex { get; set; }

    }
}