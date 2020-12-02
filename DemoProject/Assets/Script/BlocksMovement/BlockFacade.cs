using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlockFacade : ITickable
    {
        public Block Block { get; }
        public Transform BlockTransform { get; }
        public Transform GhostBlockTransform { get; }
        public Vector3 RotationPoint { get; }
        

        private BlocksMovementController _blocksMovementController;
        private GhostBlocksMovementController _ghostBlocksMovementController;
        

        public BlockFacade(BlocksMovementController blocksMovementController, GhostBlocksMovementController ghostBlocksMovementController, Block block, GhostBlock ghostBlock)
        {
            _blocksMovementController = blocksMovementController;
            _ghostBlocksMovementController = ghostBlocksMovementController;
            Block = block;
            GhostBlockTransform = ghostBlock.transform;
            BlockTransform = block.transform;
            RotationPoint = block.RotationPoint;
        }

        public void Tick()
        {
            if (!Block.gameObject.activeSelf) return;
            _blocksMovementController.Move(this);
            _ghostBlocksMovementController.Move(this);
        }
        
        public class Factory : PlaceholderFactory<BlockFacade>
        {
            
        }
    }
}