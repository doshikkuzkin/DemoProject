using System;

namespace Script.BlocksMovement
{
    public interface ILinesCleaner
    {
        event Action OnLineCompleted;
    }
}