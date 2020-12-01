using Script.GameControllersInterfaces;
using Script.Helpers;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.GameControllers
{
    public class ScoreUIController : IInitializable
    {
        private IScoreController _scoreController;
        private TMP_Text _scoreText;
        private TMP_Text _linesText;
        private TMP_Text _levelText;

        public ScoreUIController(IScoreController scoreController, TMP_Text scoreText, TMP_Text linesText, TMP_Text levelText)
        {
            _scoreController = scoreController;
            _scoreText = scoreText;
            _linesText = linesText;
            _levelText = levelText;
        }

        public void Initialize()
        {
            _scoreController.OnLevelUpdated += UpdateLevelText;
            _scoreController.OnScoreUpdated += UpdateScoreText;
        }

        private void UpdateScoreText(int lines, int score)
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