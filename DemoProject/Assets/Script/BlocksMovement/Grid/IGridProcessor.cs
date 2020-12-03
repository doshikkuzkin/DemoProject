using System;
using UnityEngine;

namespace Script.BlocksMovement
{
    public interface IGridProcessor
    {
        IGridModel GridModel { get; set; }

        event Action OnTopBorderReached;
        event Action OnBlockPlaced;
        
        void AddToGrid(Transform blockTransform);
        bool CheckMovementIsValid(Transform blockTransform);
        bool CheckIfTopBorderReached(Transform blockTransform);
        void DetachChildren(Transform blockTransform);
        void CheckForFullLines();
        void ClearGrid();

    }
}