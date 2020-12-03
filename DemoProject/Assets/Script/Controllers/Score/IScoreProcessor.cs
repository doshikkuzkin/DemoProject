namespace Script.Controllers.Score
{
    public interface IScoreProcessor
    {
        IScoreView ScoreView { get; }
        IScoreModel ScoreModel { get; }

        void DropToInitialScore();
        void UpdateScore();
        void IncrementLines();
        void IncrementScore();
        void IncrementLevel();
        bool CheckNextLevelReached();
    }
}