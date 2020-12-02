using Script.GameControllersInterfaces;
using Script.Helpers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.GameControllers
{
    public class ScoreView : IScoreView
    {
        private TMP_Text _scoreText;
        private TMP_Text _linesText;
        private TMP_Text _levelText;

        public ScoreView(TMP_Text scoreText, TMP_Text linesText, TMP_Text levelText)
        {
            _scoreText = scoreText;
            _linesText = linesText;
            _levelText = levelText;
        }

        // public void UpdateScoreText(int lines, int score)
        // {
        //     _linesText.text = lines.ToString();
        //     _scoreText.text = score.FormatScore();
        // }
        //
        // public void UpdateLevelText(int level)
        // {
        //     _levelText.text = level.ToString();
        // }

        public void DisplayScoreView(IScoreModel scoreModel)
        {
            _scoreText.text = scoreModel.CurrentScore.FormatScore();
            _linesText.text = scoreModel.CurrentLinesCount.ToString();
            _levelText.text = (scoreModel.CurrentLevelIndex + 1).ToString();
        }
    }
}