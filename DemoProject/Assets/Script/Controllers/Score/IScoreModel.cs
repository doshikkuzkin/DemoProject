namespace Script.GameControllers
{
    public interface IScoreModel
    {
        int CurrentScore { get; set; }
        int CurrentLinesCount { get; set; }
        int CurrentLevelIndex { get; set; }
    }
}