using UnityEngine;

namespace Script.BlocksMovement.Grid
{
    public interface IGridModel
    {
        Transform SpawnPoint { get; set; }
        Transform[,] Grid { get; set; }
    }
}