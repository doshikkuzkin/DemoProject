using UnityEngine;

namespace Script.BlocksMovement
{
    public class GhostBlock : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationPoint;

        public Vector3 RotationPoint => rotationPoint;
    }
}