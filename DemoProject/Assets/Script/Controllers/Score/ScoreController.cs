using System.Threading.Tasks;
using Script.Board;
using Script.ControllersCore;

namespace Script.Controllers.Score
{
    public class ScoreController : ControllerBase
    {
        private ILinesCleaner _linesCleaner;
        private IScoreProcessor _scoreProcessor;
        public ScoreController(IControllerFactory controllerFactory,
            IScoreProcessor scoreProcessor,
            ILinesCleaner board) 
            : base(controllerFactory)
        {
            _linesCleaner = board;
            _scoreProcessor = scoreProcessor;
        }

        protected override Task OnStartAsync()
        {
            _scoreProcessor.DropToInitialScore();
            _linesCleaner.OnLineCompleted += _scoreProcessor.UpdateScore;
            return Task.CompletedTask;
        }

        protected override Task OnStopAsync()
        {
            _linesCleaner.OnLineCompleted -= _scoreProcessor.UpdateScore;
            return Task.CompletedTask;
        }
    }
}