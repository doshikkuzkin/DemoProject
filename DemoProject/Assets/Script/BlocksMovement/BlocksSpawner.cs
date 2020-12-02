using System.Collections;
using Script.Installers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlocksSpawner
    {
        private readonly Transform _spawnPoint;
        private Image _blockPreviewImage;

        private BlockFacade.Factory _blockFactory;

        private BlockFacade _currentBlock;
        private BlockFacade _nextBlock;

        public BlocksSpawner(Transform spawnPoint, Image blockPreviewImage, BlockFacade.Factory blockFactory)
        {
            _spawnPoint = spawnPoint;
            _blockPreviewImage = blockPreviewImage;
            _blockFactory = blockFactory;
        }

        public void SpawnNewBlock()
        {
            _currentBlock = _nextBlock ?? _blockFactory.Create();
            _currentBlock.BlockTransform.position = _spawnPoint.transform.position;
            _currentBlock.BlockTransform.gameObject.SetActive(true);
            _currentBlock.GhostBlockTransform.gameObject.SetActive(true);

            _nextBlock = _blockFactory.Create();
            _nextBlock.BlockTransform.position = _spawnPoint.transform.position;
            _nextBlock.BlockTransform.gameObject.SetActive(false);
            _nextBlock.GhostBlockTransform.gameObject.SetActive(false);
            
            _blockPreviewImage.sprite = _nextBlock.Block.PreviewSprite;
        }
    }
    
    
}