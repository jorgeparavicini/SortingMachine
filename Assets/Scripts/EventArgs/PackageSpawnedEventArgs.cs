using UnityEngine;

namespace EventArgs
{
    public class PackageSpawnedEventArgs
    {
        public GameObject Instance { get; }
        public Vector3 Position { get; }

        public PackageSpawnedEventArgs(GameObject instance, Vector3 position)
        {
            Instance = instance;
            Position = position;
        }
    }
}