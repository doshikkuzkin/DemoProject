using System.Threading.Tasks;
using Script.BlocksMovement;
using Script.BlocksMovement.Grid;
using Script.ControllersCore;
using Script.ControllersEvents;

namespace Script.Controllers
{
    public class SpawnController : ControllerBase
    {
        private BlocksSpawner _blocksSpawner;
        private IGridProcessor _gridProcessor;
        
        public SpawnController(IControllerFactory controllerFactory,
            BlocksSpawner blocksSpawner,
            IGridProcessor gridProcessor)
            : base(controllerFactory)
        {
            _blocksSpawner = blocksSpawner;
            _gridProcessor = gridProcessor;
        }

        protected override Task OnStartAsync()
        {
            _gridProcessor.ClearGrid();
            _gridProcessor.OnBlockPlaced += _blocksSpawner.SpawnNewBlock;
            _gridProcessor.OnTopBorderReached += OnGridProcessorFilled;
            _blocksSpawner.SpawnNewBlock();
            return Task.CompletedTask;
        }

        protected override Task OnStopAsync()
        {
            _gridProcessor.OnBlockPlaced -= _blocksSpawner.SpawnNewBlock;
            _gridProcessor.OnTopBorderReached -= OnGridProcessorFilled;
            return Task.CompletedTask;
        }

        private void OnGridProcessorFilled()
        {
            DispatchBubblingEvent(new EndGameEvent());
        }
    }
}