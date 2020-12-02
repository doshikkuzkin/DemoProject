using System.Threading.Tasks;
using Script.BlocksMovement;
using Script.ControllersCore;

namespace Script.GameControllers
{
    public class MovementController : ControllerBase
    {
        private BlockMovement _blockMovement;
        private GhostBlockMovement _ghostBlockMovement;
        
        public MovementController(IControllerFactory controllerFactory,
            BlockMovement blockMovement,
            GhostBlockMovement ghostBlockMovement)
            : base(controllerFactory)
        {
            _blockMovement = blockMovement;
            _ghostBlockMovement = ghostBlockMovement;
        }

        protected override Task OnStartAsync()
        {
            _blockMovement.IsMovementEnabled = true;
            _ghostBlockMovement.IsMovementEnabled = true;
            return Task.CompletedTask;
        }

        protected override Task OnStopAsync()
        {
            _blockMovement.IsMovementEnabled = false;
            _ghostBlockMovement.IsMovementEnabled = false;
            return Task.CompletedTask;
        }
    }
}