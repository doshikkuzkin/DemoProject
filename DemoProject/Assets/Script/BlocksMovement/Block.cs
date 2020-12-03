using UnityEngine;

namespace Script.BlocksMovement
{
    public class Block : GhostBlock
    {
        [SerializeField] private Sprite previewSprite;
        public Sprite PreviewSprite => previewSprite;
    }
}