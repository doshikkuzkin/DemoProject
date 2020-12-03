using Script.Audio;
using Script.Configs;
using Script.Installers;
using UnityEngine;
using Zenject;

namespace Script.BlocksMovement
{
    public class BlockMovement : IInitializable
    {
        private Board.Board _board;
        private BlocksSpeedSettings _speedSettings;
        
        private float _secondsPassedAfterMove;
        private float _secondsBetweenMove;

        public bool IsMovementEnabled { get; set; }

        public BlockMovement(Board.Board board, BlocksSpeedSettings speedSettings)
        {
            _board = board;
            _speedSettings = speedSettings;
        }
        
        public void Initialize()
        {
            
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
            AudioPlayer.Instance.PlaySound(SoundType.MoveBlock);
            blockFacade.BlockTransform.position += movement;
            if (!_board.CheckMovementIsValid(blockFacade.BlockTransform))
            {
                blockFacade.BlockTransform.position -= movement;
            }
        }

        private void RotateByControls(BlockFacade blockFacade)
        {
            AudioPlayer.Instance.PlaySound(SoundType.RotateBlock);
            blockFacade.BlockTransform.RotateAround(blockFacade.BlockTransform.TransformPoint(blockFacade.RotationPoint), Vector3.forward, 90);
            if (!_board.CheckMovementIsValid(blockFacade.BlockTransform))
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
                if (!_board.CheckMovementIsValid(blockFacade.BlockTransform))
                {
                    AudioPlayer.Instance.PlaySound(SoundType.DropBlock);
                    blockFacade.BlockTransform.position -= Vector3.down;
                    _board.AddToGrid(blockFacade.BlockTransform);
                }
                else
                {
                    AudioPlayer.Instance.PlaySound(SoundType.MoveBlock);
                }
            }
        }
    }
}