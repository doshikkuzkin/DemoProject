using Script.GameControllersInterfaces;
using Script.Helpers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.GameControllers
{
    public class ScoreView
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

        public void UpdateScoreText(int lines, int score)
        {
            _linesText.text = lines.ToString();
            _scoreText.text = score.FormatScore();
        }

        public void UpdateLevelText(int level)
        {
            _levelText.text = level.ToString();
        }
    }
}