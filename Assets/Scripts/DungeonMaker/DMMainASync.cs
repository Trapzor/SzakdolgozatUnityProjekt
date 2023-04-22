using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GlobalControllers;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DungeonMaker
{
    public class DMMainASync : MonoBehaviour
    {
        public static event Action RestartEvent;

        [SerializeField] private List<GameObject> startPrefabs;
        [SerializeField] private List<GameObject> smallPrefabs;
        [SerializeField] private List<GameObject> cornerPrefabs;
        [SerializeField] private List<GameObject> defaultPrefabs;
        [SerializeField] private List<GameObject> endPrefabs;

        [SerializeField] private List<GameObject> placedRooms;
        [SerializeField] private List<GameObject> placedMiscRooms;
        [SerializeField] private int mintSize;
        [SerializeField] private int maxSize;

        private List<Coroutine> _coroutines;

        [SerializeField] private int tries;

        private NavMeshController _navMeshController;
        private int _dungeonSize;
        
        private void Start()
        {
            _navMeshController = gameObject.AddComponent<NavMeshController>();
            tries = 0;
        }
        
        public IEnumerator CreateDungeon()
        {
            SetDungeonSize();

            tries = 0;

            ChooseStartingRoom();
            //PlaceRooms(placedRooms[0].GetComponent<DMEntrances>().GetAllFreeEntrances(), new List<GameObject>(defaultPrefabs), 1);
            while (placedRooms.Count != _dungeonSize) 
            {
                Entrance e1 = placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances()[Random.Range(0,placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances().Count)];
                FirstStep(defaultPrefabs, placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances(), e1); 
            }

            ChooseEndingRoom();
            PlaceMiscRooms(smallPrefabs);
            ClearScene();
            //BuildNavMesh();

            DMController.Instance.SetBool(true);
            yield return null;
        }

        private void SetDungeonSize()
        {
            _dungeonSize = Random.Range(mintSize, maxSize);
        }
        
        private void ChooseStartingRoom()
        {
            int roomIndex = Random.Range(0, startPrefabs.Count);
            GameObject newRoom = InstantiateRoom(startPrefabs, roomIndex); 
            placedRooms.Add(newRoom);
        }
        void FirstStep(List<GameObject> roomPrefabs, List<Entrance> prevRoomEntrances, Entrance e1)
        {
            tries++;
            if (tries == _dungeonSize * 4)
            {
                ClearDungeon();
            }
            else
            {
                List<GameObject> availableRooms = new List<GameObject>(roomPrefabs);
                List<Entrance> prevEntrances = new List<Entrance>(prevRoomEntrances);


                int roomIndex = Random.Range(0, availableRooms.Count);
                GameObject newRoom = InstantiateRoom(availableRooms, roomIndex);

                List<Entrance> actualEntrances = newRoom.GetComponent<DMEntrances>().GetAllFreeEntrances();
                Entrance e2 = actualEntrances[Random.Range(0, actualEntrances.Count)];

                RotateToPlace(e1, e2, newRoom);
                MoveToPlace(e1, e2, newRoom);

                if (!AcceptRoom(newRoom))
                {
                    availableRooms.Remove(newRoom);
                    if (availableRooms.Count > 0)
                    {
                        newRoom.SetActive(false);
                        Destroy(newRoom);
                        //GC.Collect();

                        FirstStep(availableRooms, prevEntrances, e1);
                    }
                    else if (prevEntrances.Count > 1)
                    {
                        prevEntrances.Remove(e1);

                        newRoom.SetActive(false);
                        Destroy(newRoom);
                        //GC.Collect();

                        availableRooms = new List<GameObject>(defaultPrefabs);
                        e1 = prevEntrances[Random.Range(0, prevEntrances.Count)];

                        FirstStep(availableRooms, prevEntrances, e1);
                    }
                    else
                    {
                        GameObject prevRoom = placedRooms[^1];

                        prevRoom.GetComponent<DMEntrances>().BreakMainConnection();
                        prevRoom.SetActive(false);
                        newRoom.SetActive(false);
                        Destroy(prevRoom);
                        Destroy(newRoom);
                        // GC.Collect();

                        placedRooms.RemoveAt(placedRooms.Count - 1);
                        prevEntrances = placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances();
                        e1 = prevEntrances[Random.Range(0, prevEntrances.Count)];

                        FirstStep(availableRooms, prevEntrances, e1);
                    }
                }
                else
                {
                    newRoom.GetComponent<DMEntrances>().SetMainConnection(e1, e2);
                    placedRooms.Add(newRoom);
                }
            }
        }
        
        void ChooseEndingRoom()
        {
            GameObject roomToDelete = placedRooms[placedRooms.Count - 1];
            roomToDelete.GetComponent<DMEntrances>().BreakMainConnection();
            roomToDelete.SetActive(false);
            Destroy(roomToDelete);
            placedRooms.RemoveAt(placedRooms.Count - 1);
            // GC.Collect();
            
            int roomIndex = Random.Range(0, endPrefabs.Count);
            GameObject newRoom = InstantiateRoom(endPrefabs, roomIndex);

            Entrance e1 = placedRooms[placedRooms.Count - 1].GetComponent<DMEntrances>().GetFreeEntrance();
            Entrance e2 = newRoom.GetComponent<DMEntrances>().GetFreeEntrance();
            
            RotateToPlace(e1, e2, newRoom);
            MoveToPlace(e1, e2, newRoom);
            
            newRoom.GetComponent<DMEntrances>().SetMainConnection(e1, e2);

            placedRooms.Add(newRoom);
        }

        void PlaceMiscRooms(List<GameObject> roomPrefabs)
        {
            for (int i = 0; i < placedRooms.Count; i++)
            {
                List<Entrance> freeEntrances = placedRooms[i].GetComponent<DMEntrances>().GetAllFreeEntrances();
                for (int j = 0; j < freeEntrances.Count; j++)
                {
                    int roomIndex = Random.Range(0, roomPrefabs.Count);
                    GameObject newRoom = InstantiateRoom(roomPrefabs, roomIndex);

                    Entrance e2 = newRoom.GetComponent<DMEntrances>().GetFreeEntrance();
                    RotateToPlace(freeEntrances[j], e2, newRoom);
                    MoveToPlace(freeEntrances[j], e2, newRoom);
                    if (!AcceptRoom(newRoom))
                    {
                        newRoom.SetActive(false);
                        Destroy(newRoom);
                        //GC.Collect();

                        freeEntrances[j].CloseEntrance();
                    }
                    else
                    {
                        placedMiscRooms.Add(newRoom);
                    }
                }
            }
        } 
        
        void ClearScene()
        { 
            var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "New Game Object");
            foreach (GameObject o in objects)
            {
                o.SetActive(false);
                Destroy(o);
            }
            
        }
        
        GameObject InstantiateRoom(List<GameObject> roomPrefabs, int roomIndex)
        {
            Vector3 place = new Vector3();
            if (placedRooms.Count == 0)
                place = new Vector3(0, 0, 0);
            else
                place = new Vector3(placedRooms[^1].transform.position.x + 50, placedRooms[^1].transform.position.y,
                    placedRooms[^1].transform.position.z + 50);
            
            GameObject newRoom = new GameObject();
            newRoom = roomPrefabs[roomIndex];
            GameObject instantiated = 
                Instantiate(newRoom, place, quaternion.identity);

            return instantiated;
        }
        
        bool AcceptRoom(GameObject newRoom)
        {
            bool isIntersecting = newRoom.GetComponent<DMRoom>().GetIsIntersecting();
            if (!isIntersecting)
                return true;
            return false;
        }
        
        void MoveToPlace(Entrance e1, Entrance e2, GameObject newRoom)
        {
            Vector3 move = e1.transform.position - e2.transform.position;
            newRoom.transform.position += move;
        }

        private void RotateToPlace(Entrance e1, Entrance e2, GameObject newRoom)
        {
            if (!CorrectRotation(e1, e2))
            {
                newRoom.transform.Rotate(0, 90, 0);
                newRoom.GetComponent<DMEntrances>().RotateQuarters();
                RotateToPlace(e1, e2, newRoom);
            }
        }
        private bool CorrectRotation(Entrance e1, Entrance e2)
        {
            if (e1.quarter == DMEntranceEnum.N && e2.quarter == DMEntranceEnum.S)
                return true;
            if (e1.quarter == DMEntranceEnum.S && e2.quarter == DMEntranceEnum.N)
                return true;
            if (e1.quarter == DMEntranceEnum.W && e2.quarter == DMEntranceEnum.E)
                return true;
            if (e1.quarter == DMEntranceEnum.E && e2.quarter == DMEntranceEnum.W)
                return true;
            return false;
        }
        
        void ClearDungeon()
        {
            for(int i = 0; i < placedRooms.Count; i++)
            {
                GameObject r = placedRooms[i];
                if(i != 0)
                    r.GetComponent<DMEntrances>().BreakMainConnection();
                List<Entrance> entrances = placedRooms[i].GetComponent<DMEntrances>().GetAllEntrances();
                for (int j = 0; j < entrances.Count; j++)
                {
                    entrances[j].OpenEntrance();
                }
                r.SetActive(false);
                Destroy(r);
            }
            for(int i = 0; i < placedMiscRooms.Count; i++)
            {
                GameObject r = placedMiscRooms[i];
                r.SetActive(false);
                Destroy(r);
            }
            placedRooms.Clear();
            placedRooms.TrimExcess();
            placedMiscRooms.Clear();
            placedMiscRooms.TrimExcess();
            Resources.UnloadUnusedAssets();

            StopCoroutine(CreateDungeon());
            RestartEvent?.Invoke();
        }

        public List<GameObject> GetPlacedRooms()
        {
            return placedRooms;
        }
    }
}
