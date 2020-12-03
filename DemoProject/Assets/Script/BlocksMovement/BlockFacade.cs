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
        
        private BlockMovement _blockMovement;
        private GhostBlockMovement _ghostBlockMovement;
        
        public BlockFacade(BlockMovement blockMovement, GhostBlockMovement ghostBlockMovement, Block block, GhostBlock ghostBlock)
        {
            _blockMovement = blockMovement;
            _ghostBlockMovement = ghostBlockMovement;
            Block = block;
            GhostBlockTransform = ghostBlock.transform;
            BlockTransform = block.transform;
            RotationPoint = block.RotationPoint;
        }

        public void Tick()
        {
            if (!Block.gameObject.activeSelf) return;
            _blockMovement.Move(this);
            _ghostBlockMovement.Move(this);
        }
        
        public class Factory : PlaceholderFactory<BlockFacade>
        {
            
        }
    }
}