using UnityEngine;

namespace Script.BlocksMovement.Grid
{
    public class GridModel : IGridModel
    {
        public Transform SpawnPoint { get; set; }
        public Transform[,] Grid { get; set; }

        public GridModel(Transform spawnPoint)
        {
            SpawnPoint = spawnPoint;
        }
    }
}