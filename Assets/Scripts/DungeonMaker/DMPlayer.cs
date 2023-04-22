using UnityEngine;
using UnityEngine.AI;

namespace DungeonMaker
{
    public class DMPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        internal void SpawnPlayer(Vector3 spawnPoint)
        {
            player.GetComponent<NavMeshAgent>().Warp(spawnPoint);
            player.transform.position = spawnPoint;
        }
    }
}
