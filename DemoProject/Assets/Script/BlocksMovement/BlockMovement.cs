using Script.Audio;
using Script.Configs;
using UnityEngine;

namespace Script.BlocksMovement
{
    public class BlockMovement
    {
        private IGridProcessor _gridProcessor;
        private IAudioPlayer _audioPlayer;
        private BlocksSpeedSettings _speedSettings;
        
        private float _secondsPassedAfterMove;
        private float _secondsBetweenMove;

        public bool IsMovementEnabled { get; set; }

        public BlockMovement(IAudioPlayer audioPlayer, IGridProcessor gridProcessor, BlocksSpeedSettings speedSettings)
        {
            _gridProcessor = gridProcessor;
            _speedSettings = speedSettings;
            _audioPlayer = audioPlayer;
        }

        public void Move(BlockFacade blockFacade)
        {
            if (!IsMovementEnabled) return;
            
            HandleUserInput(blockFacade);
            MoveDownConstant(blockFacade);
        }

        private void HandleUserInput(BlockFacade blockFacade)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveByControls(blockFacade, Vector3.right);
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveByControls(blockFacade, Vector3.left);
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateByControls(blockFacade);
            }
            
            if (Input.GetKey(KeyCode.DownArrow))
            {
                ChangeMoveRate(_speedSettings.currentSpeed / 15);
            }
            else
            {
                ChangeMoveRate(_speedSettings.currentSpeed);
            }
        }

        private void MoveByControls(BlockFacade blockFacade, Vector3 movement)
        {
            _audioPlayer.PlaySound(SoundType.MoveBlock);
            blockFacade.BlockTransform.position += movement;
            if (!_gridProcessor.CheckMovementIsValid(blockFacade.BlockTransform))
            {
                blockFacade.BlockTransform.position -= movement;
            }
        }

        private void RotateByControls(BlockFacade blockFacade)
        {
            _audioPlayer.PlaySound(SoundType.RotateBlock);
            blockFacade.BlockTransform.RotateAround(blockFacade.BlockTransform.TransformPoint(blockFacade.RotationPoint), Vector3.forward, 90);
            if (!_gridProcessor.CheckMovementIsValid(blockFacade.BlockTransform))
            {
                blockFacade.BlockTransform.RotateAround(blockFacade.BlockTransform.TransformPoint(blockFacade.RotationPoint), Vector3.forward, -90);
            }
        }

        private void ChangeMoveRate(float secondsBetweenMove)
        {
            _secondsBetweenMove = secondsBetweenMove;
        }
        
        private void MoveDownConstant(BlockFacade blockFacade)
        {
            _secondsPassedAfterMove += Time.deltaTime;
            if (_secondsPassedAfterMove >= _secondsBetweenMove)
            {
                blockFacade.BlockTransform.position += Vector3.down;
                _secondsPassedAfterMove = 0;
                if (!_gridProcessor.CheckMovementIsValid(blockFacade.BlockTransform))
                {
                    _audioPlayer.PlaySound(SoundType.DropBlock);
                    blockFacade.BlockTransform.position -= Vector3.down;
                    _gridProcessor.AddToGrid(blockFacade.BlockTransform);
                    if (_gridProcessor.CheckIfTopBorderReached(blockFacade.BlockTransform))
                    {
                        _audioPlayer.PlaySound(SoundType.EndGame);
                        _gridProcessor.DetachChildren(blockFacade.BlockTransform);
                    }
                    else
                    {
                        _gridProcessor.DetachChildren(blockFacade.BlockTransform);
                        _gridProcessor.CheckForFullLines();
                    }
                }
                else
                {
                    _audioPlayer.PlaySound(SoundType.MoveBlock);
                }
            }
        }
    }
}