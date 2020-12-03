namespace Script.Controllers.Score
{
    public class ScoreModel : IScoreModel
    {
        public int CurrentScore { get; set; }
        public int CurrentLinesCount { get; set; }
        public int CurrentLevelIndex { get; set; }

    }
}