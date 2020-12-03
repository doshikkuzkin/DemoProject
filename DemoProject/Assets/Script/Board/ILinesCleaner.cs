using System;

namespace Script.Board
{
    public interface ILinesCleaner
    {
        event Action OnLineCompleted;
    }
}