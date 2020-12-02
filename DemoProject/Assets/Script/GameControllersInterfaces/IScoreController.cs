using System;

namespace Script.GameControllersInterfaces
{
    public interface IScoreController
    {
        event Action<int, int> OnScoreUpdated;
        event Action<int> OnLevelUpdated;
    }
}