// using System;
// using System.Collections.Generic;
// using System.Linq;
// using GlobalControllers;
// using Unity.Mathematics;
// using UnityEngine;
// using UnityEngine.AI;
// using Random = UnityEngine.Random;
//
// namespace DungeonMaker.OldScripts
// {
//     public class DMMain : MonoBehaviour
//     {
//         [SerializeField] private List<GameObject> startPrefabs;
//         [SerializeField] private List<GameObject> smallPrefabs;
//         [SerializeField] private List<GameObject> cornerPrefabs;
//         [SerializeField] private List<GameObject> defaultPrefabs;
//         [SerializeField] private List<GameObject> endPrefabs;
//
//         [SerializeField] private List<GameObject> placedRooms;
//         [SerializeField] private List<GameObject> placedMiscRooms;
//         [SerializeField] private int mintSize;
//         [SerializeField] private int maxSize;
//
//         [SerializeField] private int tries;
//
//         private NavMeshController _navMeshController;
//         private int _dungeonSize;
//         
//         private void Start()
//         {
//             _navMeshController = new NavMeshController();
//             tries = 0;
//         }
//         
//         public void CreateDungeon()
//         {
//             SetDungeonSize();
//             //PlaceRooms();
//             
//             ChooseStartingRoom();
//             //PlaceRooms(placedRooms[0].GetComponent<DMEntrances>().GetAllFreeEntrances(), new List<GameObject>(defaultPrefabs), 1);
//             while (placedRooms.Count != _dungeonSize) 
//             {
//                 Entrance e1 = placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances()[Random.Range(0,placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances().Count)];
//                 FirstStep(defaultPrefabs, placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances(), e1); 
//             }
//
//             ChooseEndingRoom();
//             PlaceMiscRooms(smallPrefabs);
//             ClearScene();
//             CalculateNavMesh();
//         }
//
//         private void ClearScene()
//         { 
//             var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "New Game Object");
//             foreach (GameObject o in objects)
//             {
//                 o.SetActive(false);
//                 Destroy(o);
//             }
//
//
//             //GC.Collect();
//         }
//
//         private void PlaceMiscRooms(List<GameObject> roomPrefabs)
//         {
//             for (int i = 0; i < placedRooms.Count; i++)
//             {
//                 List<Entrance> freeEntrances = placedRooms[i].GetComponent<DMEntrances>().GetAllFreeEntrances();
//                 for (int j = 0; j < freeEntrances.Count; j++)
//                 {
//                     int roomIndex = Random.Range(0, roomPrefabs.Count);
//                     GameObject newRoom = InstantiateRoom(roomPrefabs, roomIndex);
//
//                     Entrance e2 = newRoom.GetComponent<DMEntrances>().GetFreeEntrance();
//                     RotateToPlace(freeEntrances[i], e2, newRoom);
//                     MoveToPlace(freeEntrances[i], e2, newRoom);                    
//                     if (!AcceptRoom(newRoom))
//                     {
//                         newRoom.SetActive(false);
//                         Destroy(newRoom);
//                         //GC.Collect();
//                         
//                         freeEntrances[i].CloseEntrance();
//                     }
//                     else
//                     {
//                         placedMiscRooms.Add(newRoom);
//                     }
//                 }
//             }
//             // foreach (GameObject r in placedRooms)
//             // {
//             //     List<Entrance> freeEntrances = r.GetComponent<DMEntrances>().GetAllFreeEntrances();
//             //     foreach (Entrance e in freeEntrances)
//             //     {
//             //         int roomIndex = Random.Range(0, roomPrefabs.Count);
//             //         GameObject newRoom = InstantiateRoom(roomPrefabs, roomIndex);
//             //
//             //         Entrance e2 = newRoom.GetComponent<DMEntrances>().GetFreeEntrance();
//             //         RotateToPlace(e, e2, newRoom);
//             //         MoveToPlace(e, e2, newRoom);
//             //
//             //         if (!AcceptRoom(newRoom))
//             //         {
//             //             newRoom.SetActive(false);
//             //             Destroy(newRoom);
//             //             //GC.Collect();
//             //             
//             //             e.CloseEntrance();
//             //         }
//             //         else
//             //         {
//             //             placedMiscRooms.Add(newRoom);
//             //         }
//             //     }
//             //     
//             // }
//         }
//
//         void FirstStep(List<GameObject> roomPrefabs, List<Entrance> prevRoomEntrances, Entrance e1)
//         {
//             tries++;
//             if (tries == _dungeonSize * 4)
//             {
//                 ClearDungeon();
//                 
//             }
//             else
//             {
//                 List<GameObject> availableRooms = new List<GameObject>(roomPrefabs);
//             List<Entrance> prevEntrances = new List<Entrance>(prevRoomEntrances);
//             
//
//             int roomIndex = Random.Range(0, availableRooms.Count);
//             GameObject newRoom = InstantiateRoom(availableRooms, roomIndex);
//             
//             List<Entrance> actualEntrances = newRoom.GetComponent<DMEntrances>().GetAllFreeEntrances();
//             Entrance e2 = actualEntrances[Random.Range(0, actualEntrances.Count)];
//             
//             RotateToPlace(e1, e2, newRoom);
//             MoveToPlace(e1, e2, newRoom);
//
//             if (!AcceptRoom(newRoom))
//             {
//                 availableRooms.Remove(newRoom);
//                 if (availableRooms.Count > 0)
//                 {
//                     newRoom.SetActive(false);
//                     Destroy(newRoom);
//                     //GC.Collect();
//                     
//                     FirstStep(availableRooms, prevEntrances, e1);
//                 }
//                 else if(prevEntrances.Count > 1)
//                 {
//                     prevEntrances.Remove(e1);
//                     
//                     newRoom.SetActive(false);
//                     Destroy(newRoom);
//                     //GC.Collect();
//
//                     availableRooms = new List<GameObject>(defaultPrefabs);
//                     e1 = prevEntrances[Random.Range(0, prevEntrances.Count)];
//                     
//                     FirstStep(availableRooms, prevEntrances,e1);
//                 }
//                 else
//                 {
//                     GameObject prevRoom = placedRooms[^1];
//                     
//                     prevRoom.GetComponent<DMEntrances>().BreakMainConnection();
//                     prevRoom.SetActive(false);
//                     newRoom.SetActive(false);
//                     Destroy(prevRoom);
//                     Destroy(newRoom);
//                     // GC.Collect();
//                     
//                     placedRooms.RemoveAt(placedRooms.Count - 1);
//                     prevEntrances = placedRooms[^1].GetComponent<DMEntrances>().GetAllFreeEntrances();
//                     e1 = prevEntrances[Random.Range(0, prevEntrances.Count)];
//                     
//                     FirstStep(availableRooms, prevEntrances, e1);
//                 }
//             }
//             else
//             {
//                 newRoom.GetComponent<DMEntrances>().SetMainConnection(e1, e2);
//                 placedRooms.Add(newRoom);
//             } 
//             }
//                 
//             
//             
//         }
//
//         void ClearDungeon()
//         {
//             for(int i = 0; i < placedRooms.Count; i++)
//             {
//                 GameObject r = placedRooms[i];
//                 if(i != 0)
//                     r.GetComponent<DMEntrances>().BreakMainConnection();
//                 List<Entrance> entrances = placedRooms[i].GetComponent<DMEntrances>().GetAllEntrances();
//                 for (int j = 0; j < entrances.Count; j++)
//                 {
//                     entrances[j].OpenEntrance();
//                 }
//                 r.SetActive(false);
//                 Destroy(r);
//             }
//             for(int i = 0; i < placedMiscRooms.Count; i++)
//             {
//                 GameObject r = placedMiscRooms[i];
//                 r.SetActive(false);
//                 Destroy(r);
//             }
//             placedRooms.Clear();
//             placedRooms.TrimExcess();
//             placedMiscRooms.Clear();
//             placedMiscRooms.TrimExcess();
//             Resources.UnloadUnusedAssets();
//             GC.Collect();
//             CreateDungeon();
//         }
//
//         private void ChooseEndingRoom()
//         {
//             GameObject roomToDelete = placedRooms[placedRooms.Count - 1];
//             roomToDelete.GetComponent<DMEntrances>().BreakMainConnection();
//             roomToDelete.SetActive(false);
//             Destroy(roomToDelete);
//             placedRooms.RemoveAt(placedRooms.Count - 1);
//             // GC.Collect();
//             
//             int roomIndex = Random.Range(0, endPrefabs.Count);
//             GameObject newRoom = InstantiateRoom(endPrefabs, roomIndex);
//
//             Entrance e1 = placedRooms[placedRooms.Count - 1].GetComponent<DMEntrances>().GetFreeEntrance();
//             Entrance e2 = newRoom.GetComponent<DMEntrances>().GetFreeEntrance();
//             
//             RotateToPlace(e1, e2, newRoom);
//             MoveToPlace(e1, e2, newRoom);
//             
//             newRoom.GetComponent<DMEntrances>().SetMainConnection(e1, e2);
//
//             placedRooms.Add(newRoom);
//         }
//
//         private void ChooseStartingRoom()
//         {
//             int roomIndex = Random.Range(0, startPrefabs.Count);
//             GameObject newRoom = InstantiateRoom(startPrefabs, roomIndex); 
//             placedRooms.Add(newRoom);
//         }
//
//         private bool PlaceRooms(List<Entrance> entrances, List<GameObject> roomPrefabs, int step)
//         {
//             if(step >= _dungeonSize)
//                 return true;
//             
//             bool placed = false;
//             
//             while (entrances.Count > 0)
//             {
//                 int entranceIndex = Random.Range(0, entrances.Count);
//                 Entrance selectedEntrance = entrances[entranceIndex];
//                 List<GameObject> availablePrefabs = new List<GameObject>(roomPrefabs);
//
//                 while (availablePrefabs.Count > 0)
//                 {
//                     List<GameObject> rooms = TryToPlaceRoomsToEntrance(selectedEntrance, ref availablePrefabs);
//
//                     while (rooms.Count > 0)
//                     {
//                         GameObject room = rooms[Random.Range(0, rooms.Count)];
//
//                         if (step < _dungeonSize)
//                         {
//                             if (PlaceRooms(room.GetComponent<DMEntrances>().GetAllFreeEntrances(), roomPrefabs,
//                                     step + 1))
//                             {
//                                 rooms.Remove(room);
//                                 int i = rooms.Count - 1;
//                                 while (rooms.Count > 0)
//                                 {
//                                     GameObject r = rooms[i];
//                                     r.GetComponent<DMEntrances>().BreakMainConnection();
//                                     r.SetActive(false);
//                                     rooms.Remove(r);
//                                     Destroy(r);
//                                     i -= 1;
//                                 }
//                                 rooms.Clear();
//                                 placedRooms.Add(room);
//                                 
//                                 return true;
//                             }
//
//                             rooms.Remove(room);
//                             room.GetComponent<DMEntrances>().BreakMainConnection();
//                             room.SetActive(false);
//                             Destroy(room);
//                             // GC.Collect();
//                         }
//                     }
//                 }
//                 
//                 entrances.RemoveAt(entranceIndex);
//             }
//
//             return false;
//         }
//
//         private List<GameObject> TryToPlaceRoomsToEntrance(Entrance entrance, ref List<GameObject> roomPrefabs)
//         {
//             List<GameObject> temp = new List<GameObject>();
//             
//             while (roomPrefabs.Count > 0)
//             {
//                 int roomIndex = Random.Range(0, roomPrefabs.Count - 1);
//                 GameObject newRoom = InstantiateRoom(roomPrefabs, roomIndex);
//                 List<Entrance> roomEntrances = newRoom.GetComponent<DMEntrances>().GetAllFreeEntrances();
//
//                 temp = TryToPlaceRoom(entrance, roomEntrances, newRoom);
//                 
//                 roomPrefabs.RemoveAt(roomIndex);
//                 
//                 if (temp.Count > 0)
//                 {
//                     newRoom.SetActive(false);
//                     Destroy(newRoom);
//                     // GC.Collect();
//                     break;
//                 }
//                 // newRoom.SetActive(false);
//                 // Destroy(newRoom);
//                 // GC.Collect();
//             }
//
//             return temp;
//         }
//
//         private List<GameObject> TryToPlaceRoom(Entrance entrance, List<Entrance> roomEntrances, GameObject room)
//         {
//             List<GameObject> temp = new List<GameObject>();
//
//             for (int i = 0; i < roomEntrances.Count; i++)
//             {
//                 Entrance roomEntrance = roomEntrances[i];
//                 
//                 RotateToPlace(entrance, roomEntrance, room);
//                 MoveToPlace(entrance, roomEntrance, room);
//                 
//                 if (AcceptRoom(room))
//                 {
//                     GameObject newRoom = Instantiate(room);
//                     newRoom.GetComponent<DMEntrances>().SetMainConnection(entrance, newRoom.GetComponent<DMEntrances>().GetAllFreeEntrances()[i]);
//                     temp.Add(newRoom);
//                 }
//             }
//
//             return temp;
//         }
//
//         private void PlaceRooms()
//         {
//             int roomIndex = Random.Range(0, startPrefabs.Count - 1);
//             GameObject newRoom = InstantiateRoom(startPrefabs, roomIndex); 
//             placedRooms.Add(newRoom);
//             
//             while(placedRooms.Count != _dungeonSize)
//             {
//                 Entrance e1 = placedRooms[^1].GetComponent<DMEntrances>().GetFreeEntrance();
//                 TryToPlace(defaultPrefabs, e1);
//             }
//         }
//
//         private void TryToPlace(List<GameObject> roomPrefabs, Entrance e1)
//         {
//             List<GameObject> availableRooms = new List<GameObject>();
//             for (int i = 0; i < roomPrefabs.Count; i++)
//             {
//                 availableRooms.Add(roomPrefabs[i]);
//             }
//             // foreach (GameObject room in roomPrefabs )
//             // {
//             //     availableRooms.Add(room);
//             // }
//             
//             int roomIndex = Random.Range(0, availableRooms.Count - 1);
//             GameObject newRoom = InstantiateRoom(availableRooms, roomIndex);
//             
//             Entrance e2 = newRoom.GetComponent<DMEntrances>().GetFreeEntrance();    
//             RotateToPlace(e1, e2, newRoom);
//             MoveToPlace(e1, e2, newRoom);
//             
//             if (AcceptRoom(newRoom) == false)
//             {
//                 Debug.Log("Hadtochagnges");
//                 if (availableRooms.Count == 1)
//                 {
//                     placedRooms[^1].GetComponent<DMEntrances>().BreakMainConnection();
//                     GameObject temp = placedRooms[^1];
//                     temp.SetActive(false);
//                     placedRooms.RemoveAt(placedRooms.Count - 1);
//                     newRoom.SetActive(false);
//                     Destroy(temp);
//                     Destroy(newRoom);
//                     // GC.Collect();
//                     
//                     //TryToPlace(defaultPrefabs, placedRooms[^1].GetComponent<DMEntrances>().GetFreeEntrance());
//                 }
//                 else
//                 {
//                     availableRooms.RemoveAt(roomIndex);
//                     newRoom.SetActive(false);
//                     Destroy(newRoom);
//                     // GC.Collect();
//                     
//                     TryToPlace(availableRooms, e1);
//                 }
//             }
//             else
//             {
//                 newRoom.GetComponent<DMEntrances>().SetMainConnection(e1, e2);
//                 placedRooms.Add(newRoom);
//             }
//         }
//
//         private void MoveToPlace(Entrance e1, Entrance e2, GameObject newRoom)
//         {
//             Vector3 move = e1.transform.position - e2.transform.position;
//             newRoom.transform.position += move;
//         }
//
//         private void RotateToPlace(Entrance e1, Entrance e2, GameObject newRoom)
//         {
//             if (!CorrectRotation(e1, e2))
//             {
//                 newRoom.transform.Rotate(0, 90, 0);
//                 newRoom.GetComponent<DMEntrances>().RotateQuarters();
//                 RotateToPlace(e1, e2, newRoom);
//             }
//         }
//
//         private bool CorrectRotation(Entrance e1, Entrance e2)
//         {
//             if (e1.quarter == DMEntranceEnum.N && e2.quarter == DMEntranceEnum.S)
//                 return true;
//             if (e1.quarter == DMEntranceEnum.S && e2.quarter == DMEntranceEnum.N)
//                 return true;
//             if (e1.quarter == DMEntranceEnum.W && e2.quarter == DMEntranceEnum.E)
//                 return true;
//             if (e1.quarter == DMEntranceEnum.E && e2.quarter == DMEntranceEnum.W)
//                 return true;
//             return false;
//         }
//         bool AcceptRoom(GameObject newRoom)
//         {
//             bool isIntersecting = newRoom.GetComponent<DMRoom>().GetIsIntersecting();
//             if (!isIntersecting)
//                 return true;
//             return false;
//         }
//         GameObject InstantiateRoom(List<GameObject> roomPrefabs, int roomIndex)
//         {
//             Vector3 place = new Vector3();
//             if (placedRooms.Count == 0)
//                 place = new Vector3(0, 0, 0);
//             else
//                 place = new Vector3(placedRooms[^1].transform.position.x + 50, placedRooms[^1].transform.position.y,
//                     placedRooms[^1].transform.position.z + 50);
//             
//             GameObject newRoom = new GameObject();
//             newRoom = roomPrefabs[roomIndex];
//             GameObject instantiated = 
//                 Instantiate(newRoom, place, quaternion.identity);
//             
//             return instantiated;
//         }
//
//         private void SetDungeonSize()
//         {
//             _dungeonSize = Random.Range(mintSize, maxSize);
//         }
//
//         private void CalculateNavMesh()
//         {
//             NavMeshSurface navMeshSurface = placedRooms[0].GetComponent<NavMeshSurface>();
//             _navMeshController.BakeNavMesh(navMeshSurface);
//         }
//
//         public List<GameObject> GetPlacedRooms()
//         {
//             return placedRooms;
//         }
//     }
// }
