using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class GhostBlocksMovementController : ITickable
    {
        private BlockFacade _blockFacade;
        private Board _board;

        public GhostBlocksMovementController(BlockFacade blockFacade, Board board)
        {
            _board = board;
            _blockFacade = blockFacade;
        }
        public void Tick()
        {
            if (_blockFacade == null) return;
            
            _blockFacade.GhostTransform.position = _blockFacade.Transform.position;
            _blockFacade.GhostTransform.rotation = _blockFacade.Transform.rotation;

            while (_board.CheckMovementIsValid(_blockFacade.GhostTransform))
            {
                _blockFacade.GhostTransform.position += Vector3.down;
            }

            if (!_board.CheckMovementIsValid(_blockFacade.GhostTransform))
            {
                _blockFacade.GhostTransform.position += Vector3.up;
            }
        }
    }
}