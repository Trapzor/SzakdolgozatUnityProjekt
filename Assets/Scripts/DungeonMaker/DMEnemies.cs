using System;
using System.Collections.Generic;
using System.Linq;
using EnemyScripts;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace DungeonMaker
{
    public class DMEnemies : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> smallEnemies;
        
        [SerializeField]
        private List<GameObject> bigEnemies;

        private List<GameObject> spawnedEnemies;

        private void Start()
        {
            spawnedEnemies = new List<GameObject>();
        }

        public void SpawnEnemies(List<GameObject> rooms)
        {
            for (int i = 1; i < rooms.Count; i++)
            {
                int rnd = Random.Range(1, 11);
                if (rnd > 2)
                    SpawnSmallEnemies(rooms[i]);
                if(rnd > 7)
                    SpawnBigEnemies(rooms[i]);
            }

            RelocateEnemies();
            
            ClearScene();
        }

        private void RelocateEnemies()
        {
            foreach (GameObject enemy in spawnedEnemies)
            {
                NavMeshAgent enemyNM = enemy.GetComponent<NavMeshAgent>();
                enemyNM.Warp(enemy.GetComponent<EnemyAI>().GetStartingPosition());
                enemyNM.SetDestination(enemy.transform.position);
            }
        }

        void SpawnSmallEnemies(GameObject room)
        {
            int rnd = Random.Range(1, 6);
            int enemyIndex = Random.Range(0, smallEnemies.Count - 1);
            for (int i = 0; i < rnd; i++)
            {
                SpawnChosen(smallEnemies, enemyIndex, room);
            }
        }

        void SpawnBigEnemies(GameObject room)
        {
            int rnd = Random.Range(1, 3);
            int enemyIndex = Random.Range(0, bigEnemies.Count - 1);
            {
                SpawnChosen(bigEnemies, enemyIndex, room);
            }   
        }

        private void SpawnChosen(List<GameObject> enemyList, int index, GameObject room)
        {
            GameObject newEnemy = enemyList[index];
            GameObject instantiated = Instantiate(newEnemy, Vector3.zero , Quaternion.identity);

            Vector3 startPos = RandomSpawnPlace(room);
            instantiated.GetComponent<EnemyAI>().SetStartingPosition(startPos);

            spawnedEnemies.Add(instantiated);
        }
        
        private void ClearScene()
        { 
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "New Game Object");
            foreach (GameObject o in objects)
            {
                o.SetActive(false);
                Destroy(o);
            }

            GC.Collect();
        }

        private Vector3 RandomSpawnPlace(GameObject room)
        {
            BoxCollider spawnArea = room.GetComponent<DMRoom>().GetSpawnArea();
            Vector3 randomPlace = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            return randomPlace;
        }
    }
}
