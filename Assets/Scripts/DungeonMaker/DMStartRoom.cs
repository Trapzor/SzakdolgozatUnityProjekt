using UnityEngine;

namespace DungeonMaker
{
    public class DMStartRoom : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        private Vector3 spawnPoint;
        private void Start()
        {
            spawnPoint = spawn.transform.position;
        }

        public Vector3 GetSpawnPoint()
        {
            return spawnPoint;
        }
    }
}
