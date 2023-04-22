// using System.Collections.Generic;
// using CommonProperties;
// using UnityEngine;
// using UnityEngine.AI;
// using Random = UnityEngine.Random;
//
// namespace DungeonMaker.OldsScripts
// {
//     public class DMMaster : MonoBehaviour
//     {
//         [SerializeField] private List<GameObject> startPrefabs;
//         [SerializeField] private List<GameObject> smallPrefabs;
//         [SerializeField] private List<GameObject> cornerRooms;
//         [SerializeField] private List<GameObject> defaultPrefabs;
//         [SerializeField] private List<GameObject> endPrefabs;
//         
//         [SerializeField] private List<GameObject> rooms;
//         [SerializeField] private int minSize;
//         [SerializeField] private int maxSize;
//         
//         NavMeshController NavMeshController;
//     
//         private int size;
//     
//         private void Start()
//         {
//             NavMeshController = new NavMeshController();
//             CreateDungeon();
//         }
//
//         private void CreateDungeon()
//         {
//             SetSize();
//             PlaceRooms();
//             BuildNavMesh();
//         }
//
//         private void PlaceRooms()
//         {
//             PlaceStartRoom();
//             
//             int i = 0;
//             while (i != size)
//             { 
//                 PlaceIntermediateRooms();
//                 i++;
//             }
//             PlaceEndRoom();
//             PlaceRoomsToEmptyDoors();
//         }
//
//         private void PlaceRoomsToEmptyDoors()
//         {
//             List<GameObject> miscRooms = new List<GameObject>();
//             foreach (GameObject room in rooms)
//             {
//                 while(room.GetComponent<DMEntrances>().GetFreeEntrance() != null)
//                 {
//                     GameObject thisRoom = InstantiateRoom(smallPrefabs);
//
//                     if (!DecideIfCorrect(room, thisRoom))
//                     {
//                         Destroy(thisRoom);
//                         //TODO ADD WALL
//                     }
//                     
//                     miscRooms.Add(thisRoom);
//                 }
//             }
//             foreach (GameObject room in miscRooms)
//                 rooms.Add(room);
//         }
//
//         private void PlaceEndRoom()
//         {
//             GameObject previousRoom = rooms[^1];
//             GameObject thisRoom = InstantiateRoom(endPrefabs);
//
//             if (!DecideIfCorrect(previousRoom, thisRoom))
//             {
//                 Destroy(thisRoom);
//                 PlaceEndRoom();   
//             }
//             rooms.Add(thisRoom);
//         }
//
//         private bool DecideIfCorrect(GameObject previousRoom, GameObject thisRoom)
//         {
//             
//             Entrance e1 = previousRoom.GetComponent<DMEntrances>().GetFreeEntrance();
//             Entrance e2 = thisRoom.GetComponent<DMEntrances>().GetFreeEntrance();
//             
//             if(!CompareEntrances(e1, e2))
//                 RotateRoomRec(e1, e2, thisRoom);
//
//             bool movedCorrectly = MoveRoomToPosition(e1, e2, thisRoom);
//
//             if (!movedCorrectly)
//             {
//                 return false;
//             }
//             
//             e1.SetConnected();
//             e2.SetConnected();
//                 
//             return true;
//         }
//
//
//         private void PlaceIntermediateRooms()
//         {
//             GameObject previousRoom = rooms[^1];
//             GameObject thisRoom = InstantiateRoom(defaultPrefabs);
//
//             if(!DecideIfCorrect(previousRoom, thisRoom))
//             {
//                 PlaceCorrectRoom(previousRoom, thisRoom, 0);
//             }
//             rooms.Add(thisRoom);
//         }
//
//         void PlaceCorrectRoom(GameObject previousRoom, GameObject thisRoom, int tries)
//         {
//             Destroy(thisRoom);
//             if (cornerRooms.Count == tries)
//             {
//                 rooms.Remove(previousRoom);
//                 Destroy(previousRoom);
//                 PlaceIntermediateRooms();
//             }
//
//             thisRoom = Instantiate(cornerRooms[tries], Vector3.zero, Quaternion.identity);
//
//             if (!DecideIfCorrect(previousRoom, thisRoom))
//             {
//                 PlaceCorrectRoom(previousRoom, thisRoom, tries + 1);
//             }
//         }
//
//         private bool MoveRoomToPosition(Entrance e1, Entrance e2, GameObject thisRoom)
//         {
//             Vector3 move = e1.transform.position - e2.transform.position;
//             thisRoom.transform.position += move;
//
//             if(thisRoom.GetComponent<BorderChecker>().GetCollision())
//                 return false;
//             return true;
//         }
//
//         private void PlaceStartRoom()
//         {
//             GameObject thisRoom = InstantiateRoom(startPrefabs);
//             rooms.Add(thisRoom);
//             Entrance e1 = thisRoom.GetComponent<DMEntrances>().GetFreeEntrance();
//         }
//
//         private int GetRandomRoom(List<GameObject> prefabList)
//         {
//             return Random.Range(0, prefabList.Count);
//         }
//         void SetSize()
//         {
//             size = Random.Range(minSize, maxSize);
//         }
//         private bool CompareEntrances(Entrance e1, Entrance e2)
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
//
//         private void RotateRoomRec(Entrance e1, Entrance e2, GameObject room)
//         {
//             if (!CompareEntrances(e1, e2))
//             {
//                 room.transform.Rotate(0, 90,0);
//                 room.GetComponent<DMEntrances>().RotateQuarters();
//                 RotateRoomRec(e1,e2,room);
//             }
//         }
//         private void BuildNavMesh()
//         {
//             NavMeshSurface navMeshSurface = rooms[0].GetComponent<NavMeshSurface>();
//             NavMeshController.BakeNavMesh(navMeshSurface);
//         }
//
//         GameObject InstantiateRoom (List<GameObject> roomList)
//         {
//             int roomIndex = GetRandomRoom(roomList);
//             
//             GameObject newRoom = new GameObject();
//             newRoom = roomList[roomIndex];
//             GameObject instantiate = Instantiate(newRoom, Vector3.zero, Quaternion.identity);
//             
//             return instantiate;
//         }
//     }
// }
