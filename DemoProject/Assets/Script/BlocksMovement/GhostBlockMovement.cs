using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class GhostBlockMovement
    {
        private Board.Board _board;
        
        public bool IsMovementEnabled { get; set; }

        public GhostBlockMovement(Board.Board board)
        {
            _board = board;
        }
        public void Move(BlockFacade blockFacade)
        {
            if (!IsMovementEnabled) return;

            blockFacade.GhostBlockTransform.position = blockFacade.BlockTransform.position;
            blockFacade.GhostBlockTransform.rotation = blockFacade.BlockTransform.rotation;

            while (_board.CheckMovementIsValid(blockFacade.GhostBlockTransform))
            {
                blockFacade.GhostBlockTransform.position += Vector3.down;
            }

            if (!_board.CheckMovementIsValid(blockFacade.GhostBlockTransform))
            {
                blockFacade.GhostBlockTransform.position += Vector3.up;
            }
        }
    }
}