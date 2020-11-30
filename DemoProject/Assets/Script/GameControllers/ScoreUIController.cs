using Script.Helpers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.GameControllers
{
    public class ScoreUIController : IInitializable
    {
        private ScoreController _scoreController;
        private TMP_Text _scoreText;
        private TMP_Text _linesText;
        private TMP_Text _levelText;

        public ScoreUIController(ScoreController scoreController, TMP_Text scoreText, TMP_Text linesText, TMP_Text levelText)
        {
            _scoreController = scoreController;
            _scoreText = scoreText;
            _linesText = linesText;
            _levelText = levelText;
        }

        public void Initialize()
        {
            _scoreController.OnLevelChanged += UpdateLevelText;
            _scoreController.OnLinesAndScoreCountUpdated += UpdateLinesAndScoreText;
        }

        private void UpdateLinesAndScoreText(int lines, int score)
        {
            _linesText.text = lines.ToString();
            _scoreText.text = score.FormatScore();
        }

        private void UpdateLevelText(int level, float secondsBetweenMove)
        {
            _levelText.text = level.ToString();
        }
    }
}