using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlockFacade : ITickable
    {
        private BlocksMovementController _blocksMovementController;
        private GhostBlocksMovementController _ghostBlocksMovementController;
        
        public Transform Transform { get; }
        public Transform GhostTransform { get; }
        public Block Block { get; }
        public GhostBlock GhostBlock { get; }
        public Vector3 RotationPoint { get; }
        public bool IsDisabled { get; set; }

        public BlockFacade(BlocksMovementController blocksMovementController, GhostBlocksMovementController ghostBlocksMovementController, Block block, GhostBlock ghostBlock)
        {
            _blocksMovementController = blocksMovementController;
            _ghostBlocksMovementController = ghostBlocksMovementController;
            Block = block;
            GhostBlock = ghostBlock;
            Transform = block.transform;
            RotationPoint = block.RotationPoint;
            GhostTransform = ghostBlock.transform;
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