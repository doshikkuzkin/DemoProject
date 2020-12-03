using UnityEngine;

namespace Script.BlocksMovement
{
    public interface IGridModel
    {
        Transform SpawnPoint { get; set; }
        Transform[,] Grid { get; set; }
    }
}