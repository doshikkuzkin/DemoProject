using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class GhostBlocksMovementController
    {
        private Board _board;

        public GhostBlocksMovementController(Board board)
        {
            _board = board;
        }
        public void Move(BlockFacade blockFacade)
        {
            if (blockFacade == null) return;
            
            blockFacade.GhostTransform.position = blockFacade.Transform.position;
            blockFacade.GhostTransform.rotation = blockFacade.Transform.rotation;

            while (_board.CheckMovementIsValid(blockFacade.GhostTransform))
            {
                blockFacade.GhostTransform.position += Vector3.down;
            }

            if (!_board.CheckMovementIsValid(blockFacade.GhostTransform))
            {
                blockFacade.GhostTransform.position += Vector3.up;
            }
        }
    }
}