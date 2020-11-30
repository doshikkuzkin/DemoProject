using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlockFacade
    {
        public Transform Transform { get; }
        public Transform GhostTransform { get; }
        public Block Block { get; }
        public GhostBlock GhostBlock { get; }
        public Vector3 RotationPoint { get; }
        public bool IsDisabled { get; set; }


        public BlockFacade(Block block, GhostBlock ghostBlock)
        {
            Block = block;
            GhostBlock = ghostBlock;
            Transform = block.transform;
            RotationPoint = block.RotationPoint;
            GhostTransform = ghostBlock.transform;

            IsDisabled = true;
        }
        
        public class Factory : PlaceholderFactory<BlockFacade>
        {
            
        }
    }
}