using NSubstitute;
using NUnit.Framework;
using Script.BlocksMovement.Grid;
using Script.Configs;
using UnityEngine;
using Zenject;

namespace Tests
{
    [TestFixture]
    public class TestGridProcessor : ZenjectUnitTestFixture
    {
        #region TestsData
        private static readonly (Vector3, bool)[] BlockPositions =
        {
            (new Vector3(4,3,0), true),
            (new Vector3(5,3,0), true),
            (new Vector3(6,3,0), false),
            (new Vector3(2,3,0), true),
            (new Vector3(0,3,0), true),
            (new Vector3(3,4,0), true),
            (new Vector3(3,6,0), false),
            (new Vector3(3,7,0), false),
            (new Vector3(3,2,0), true),
            (new Vector3(3,0,0), true),
        };
        
        private static readonly (Vector3, bool)[] CheckTopBorderData =
        {
            (new Vector3(4,0,0), false),
            (new Vector3(5,1,0), false),
            (new Vector3(6,2,0), false),
            (new Vector3(2,3,0), false),
            (new Vector3(0,4,0), true),
            (new Vector3(3,5,0), true),
        };
        #endregion
        
        #region Setup
        
        [Inject] private GridProcessor _gridProcessor;
        [Inject] private GridModel _gridModel;
        [Inject] private BoardSettings _boardSettings;
        
        private ILinesCleaner _linesCleaner;
        private Transform _spawnPoint;
        private Transform _block;
        private Transform _firstChild;
        private Transform _secondChild;
        private Transform _thirdChild;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _spawnPoint = new GameObject().transform;
            _spawnPoint.position = new Vector3(3,5,0);
            _block = new GameObject().transform;
            _block.transform.position = new Vector3(3,3,0);
            _firstChild = new GameObject().transform;
            _firstChild.transform.position = new Vector3(3,3,0);
            _firstChild.parent = _block;
            _secondChild = new GameObject().transform;
            _secondChild.transform.position = new Vector3(4,3,0);
            _secondChild.parent = _block;
            _thirdChild = new GameObject().transform;
            _thirdChild.transform.position = new Vector3(4,4,0);
            _thirdChild.parent = _block;

            Container.BindInterfacesAndSelfTo<GridProcessor>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridModel>().AsSingle().WithArguments(_spawnPoint);
            Container.Bind<BoardSettings>().AsSingle();
            _linesCleaner = Substitute.For<ILinesCleaner>();
            Container.Bind<ILinesCleaner>().FromInstance(_linesCleaner);

            Container.Inject(this);

            _boardSettings.boardBottomBoundary = 0;
            _boardSettings.boardTopBoundary = 6;
            _boardSettings.boardLeftBoundary = 0;
            _boardSettings.boardRightBoundary = 6;
            _gridModel.Grid = new Transform[_boardSettings.boardRightBoundary + 1, _boardSettings.boardTopBoundary + 1];
        }
        #endregion
        
        [Test, TestCaseSource(nameof(BlockPositions))]
        public void CheckMovementIsValid_CorrectCheck((Vector3 blockNewPosition, bool expectedResult)data)
        {
            _block.position = data.blockNewPosition;
            Debug.Log(_block.position);
            Debug.Log(_firstChild.position);
            Debug.Log(_secondChild.position);
            Assert.AreEqual(data.expectedResult, _gridProcessor.CheckMovementIsValid(_block));
        }
        
        [Test, TestCaseSource(nameof(CheckTopBorderData))]
        public void CheckIfTopBorderReached_CorrectCheck((Vector3 blockNewPosition, bool expectedResult)data)
        {
            _block.position = data.blockNewPosition;
            Assert.AreEqual(data.expectedResult, _gridProcessor.CheckIfTopBorderReached(_block));
        }
    }
}
