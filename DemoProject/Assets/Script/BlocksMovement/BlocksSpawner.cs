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
            _currentBlock.Transform.position = _spawnPoint.transform.position;
            _currentBlock.IsDisabled = false;
            _currentBlock.Transform.gameObject.SetActive(true);
            _currentBlock.GhostTransform.gameObject.SetActive(true);

            _nextBlock = _blockFactory.Create();
            _nextBlock.Transform.position = _spawnPoint.transform.position;
            _nextBlock.Transform.gameObject.SetActive(false);
            _nextBlock.GhostTransform.gameObject.SetActive(false);
            
            _blockPreviewImage.sprite = _nextBlock.Block.PreviewSprite;
        }
    }
    
    
}