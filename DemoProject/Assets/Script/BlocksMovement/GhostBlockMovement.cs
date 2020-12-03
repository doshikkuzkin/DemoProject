using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class GhostBlockMovement
    {
        private IGridProcessor _gridProcessor;
        
        public bool IsMovementEnabled { get; set; }

        public GhostBlockMovement(IGridProcessor gridProcessorProcessor)
        {
            _gridProcessor = gridProcessorProcessor;
        }
        public void Move(BlockFacade blockFacade)
        {
            if (!IsMovementEnabled) return;

            blockFacade.GhostBlockTransform.position = blockFacade.BlockTransform.position;
            blockFacade.GhostBlockTransform.rotation = blockFacade.BlockTransform.rotation;

            while (_gridProcessor.CheckMovementIsValid(blockFacade.GhostBlockTransform))
            {
                blockFacade.GhostBlockTransform.position += Vector3.down;
            }

            if (!_gridProcessor.CheckMovementIsValid(blockFacade.GhostBlockTransform))
            {
                blockFacade.GhostBlockTransform.position += Vector3.up;
            }
        }
    }
}