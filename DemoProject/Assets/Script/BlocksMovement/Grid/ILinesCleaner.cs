using System;

namespace Script.BlocksMovement.Grid
{
    public interface ILinesCleaner
    {
        event Action OnLineCompleted;
        bool HasFullLine(int lineIndex);
    }
}