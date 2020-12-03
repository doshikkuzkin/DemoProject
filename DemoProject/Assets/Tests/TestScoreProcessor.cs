using NSubstitute;
using NUnit.Framework;
using Script.Configs;
using Script.Controllers.Score;
using Script.ControllersCore;
using Zenject;

namespace Tests
{
    [TestFixture]
    public class TestScoreProcessor : ZenjectUnitTestFixture
    {
        #region TestsData
        private static readonly (int, int)[] ScoreForLineData =
        {
            (1, 1),
            (2, 2),
            (3, 3),
            (4, 5),
            (5, 7),
            (6, 9),
            (7, 12)
        };
        
        private static readonly (int, int)[] LevelForLinesData =
        {
            (1, 0),
            (2, 0),
            (3, 1),
            (4, 1),
            (5, 1),
            (6, 2),
            (7, 2),
            (11, 2)
        };
        
        private static readonly (int, int, bool)[] CheckReachedLevelData =
        {
            (0, 1, false),
            (0, 3, true),
            (1, 4, false),
            (1, 6, true),
            (2, 7, false),
            (2, 20, false)
        };
        #endregion

        #region Setup
        
        [Inject]
        private DifficultyLevelsConfig _difficultyLevelsConfig;
        [Inject]
        private BlocksSpeedSettings _blocksSpeedSettings;
        [Inject]
        private ScoreModel _scoreModel;
        [Inject]
        private ScoreProcessor _scoreProcessor;
        private IScoreView _scoreView;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            Container.Bind<DifficultyLevelsConfig>().AsSingle();
            Container.Bind<BlocksSpeedSettings>().AsSingle();
            Container.Bind<ScoreController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ControllerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreProcessor>().AsSingle();

            _scoreView = Substitute.For<IScoreView>();
            Container.Bind<IScoreView>().FromInstance(_scoreView);

            Container.Inject(this);
            
            _difficultyLevelsConfig.levels = new DifficultyLevel[3];
            _difficultyLevelsConfig.levels[0] = new DifficultyLevel();
            _difficultyLevelsConfig.levels[0].linesToComplete = 3;
            _difficultyLevelsConfig.levels[0].scorePointsForLine = 1;
            _difficultyLevelsConfig.levels[0].secondsBetweenBlockMove = 1;
            _difficultyLevelsConfig.levels[1] = new DifficultyLevel();
            _difficultyLevelsConfig.levels[1].linesToComplete = 6;
            _difficultyLevelsConfig.levels[1].scorePointsForLine = 2;
            _difficultyLevelsConfig.levels[1].secondsBetweenBlockMove = 1;
            _difficultyLevelsConfig.levels[2] = new DifficultyLevel();
            _difficultyLevelsConfig.levels[2].linesToComplete = 10;
            _difficultyLevelsConfig.levels[2].scorePointsForLine = 3;
            _difficultyLevelsConfig.levels[2].secondsBetweenBlockMove = 1;

            _blocksSpeedSettings.currentSpeed = _difficultyLevelsConfig.levels[0].secondsBetweenBlockMove;
        }
        
        #endregion

        [Test]
        public void UpdateScore_CorrectLineUpdate([Values(1,2,10)]int numberOfUpdates)
        {
            for (int i = 0; i < numberOfUpdates; i++)
            {
                _scoreProcessor.UpdateScore();
            }
            Assert.AreEqual(numberOfUpdates, _scoreModel.CurrentLinesCount); 
        }
        
        
        [Test, TestCaseSource(nameof(ScoreForLineData))]
        public void UpdateScore_CorrectScoreUpdate((int numberOfUpdates, int expectedResult) data)
        {
            for (int i = 0; i < data.numberOfUpdates; i++)
            {
                _scoreProcessor.UpdateScore();
            }
            Assert.AreEqual(data.expectedResult, _scoreModel.CurrentScore);
        }
        
        [Test, TestCaseSource(nameof(LevelForLinesData))]
        public void UpdateScore_CorrectLevelUpdate((int numberOfUpdates, int expectedResult) data)
        {
            for (int i = 0; i < data.numberOfUpdates; i++)
            {
                _scoreProcessor.UpdateScore();
            }
            Assert.AreEqual(data.expectedResult, _scoreModel.CurrentLevelIndex);
        }

        [Test, TestCaseSource(nameof(CheckReachedLevelData))]
        public void CheckNextLevelReached_IsCorrectCheck((int currentLevelIndex, int currentLinesCount, bool expectedResult) data)
        {
            _scoreModel.CurrentLevelIndex = data.currentLevelIndex;
            _scoreModel.CurrentLinesCount = data.currentLinesCount;
            Assert.AreEqual(data.expectedResult, _scoreProcessor.CheckNextLevelReached());
        }

        
    }
}
