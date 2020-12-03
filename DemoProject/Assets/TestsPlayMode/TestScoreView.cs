using System.Collections;
using NUnit.Framework;
using Script.Controllers.Score;
using Script.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests
{
    public class TestScoreView : SceneTestFixture
    {

        [UnityTest]
        public IEnumerator UpdateScore_ScoreTextIsUpdated([Values(0,1,2,3,4,5)]int numberOfUpdates)
        {
            yield return LoadScene("Game");
            var scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
            var scoreModel = SceneContainer.Resolve<ScoreModel>();
            UpdateScore(numberOfUpdates);
            Assert.AreEqual(scoreModel.CurrentScore.FormatScore(), scoreText.text);
        }
        
        [UnityTest]
        public IEnumerator UpdateScore_LevelTextIsUpdated([Values(0,1,2,3,4,5)]int numberOfUpdates)
        {
            yield return LoadScene("Game");
            var scoreText = GameObject.Find("LevelsText").GetComponent<TMP_Text>();
            var scoreModel = SceneContainer.Resolve<ScoreModel>();
            UpdateScore(numberOfUpdates);
            Assert.AreEqual((scoreModel.CurrentLevelIndex + 1).ToString(), scoreText.text);
        }
        
        [UnityTest]
        public IEnumerator UpdateScore_LinesTextIsUpdated([Values(0,1,2,3,4,5)]int numberOfUpdates)
        {
            yield return LoadScene("Game");
            var scoreText = GameObject.Find("LinesText").GetComponent<TMP_Text>();
            var scoreModel = SceneContainer.Resolve<ScoreModel>();
            UpdateScore(numberOfUpdates);
            Assert.AreEqual((scoreModel.CurrentLinesCount).ToString(), scoreText.text);
        }

        private void UpdateScore(int numberOfUpdates)
        {
            var scoreProcessor = SceneContainer.Resolve<ScoreProcessor>();
            for (int i = 0; i < numberOfUpdates; i++)
            {
                scoreProcessor.UpdateScore();
            }
        }
    }
}
