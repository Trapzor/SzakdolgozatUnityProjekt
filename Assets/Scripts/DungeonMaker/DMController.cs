using System;
using System.Collections.Generic;
using GlobalControllers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace DungeonMaker
{
    public class DMController : MonoBehaviour
    {
        public static DMController Instance;
        public static event Action NavMeshFinishedEvent;
        
        [SerializeField] 
        private DMMainASync baseGenerationASync;
        [SerializeField]
        private DMEnemies enemyGeneration;
        [SerializeField]
        private DMPlayer playerGeneration;

        private bool isDone;
        
        private NavMeshController _navMeshController;

        private void Awake()
        {
            _navMeshController = gameObject.AddComponent<NavMeshController>();
            Instance = this;
            isDone = false;
            DMMainASync.RestartEvent += StartCreating;
        }

        private void OnDestroy()
        {
            DMMainASync.RestartEvent -= StartCreating;
        }

        private void Start()
        {
            StartCreating();
        }

        public void StartCreating()
        {
            isDone = false;
            StartCoroutine(baseGenerationASync.CreateDungeon());
            isDone = true;
            if (isDone)
            {
                List<GameObject> rooms = baseGenerationASync.GetPlacedRooms();
                BuildNavMesh(rooms[0]);
                enemyGeneration.SpawnEnemies(rooms);
                Vector3 spawnPoint = rooms[0].GetComponent<DMStartRoom>().GetSpawnPoint();
                playerGeneration.SpawnPlayer(spawnPoint);
                
                NavMeshFinishedEvent?.Invoke();
            }

        }
        
        private void BuildNavMesh(GameObject room)
        {
            NavMeshSurface navMeshSurface = room.GetComponent<NavMeshSurface>();
            _navMeshController.BakeNavMesh(navMeshSurface);
        }

        public void SetBool(bool value)
        {
            isDone = value;
        }
    }
}
