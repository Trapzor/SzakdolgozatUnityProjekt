/*using System.Collections.Generic;
using DungeonMaker;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class DungeonMakerMaster : MonoBehaviour
{
    [SerializeField] private List<GameObject> dungeonPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> dungeonPieces = new List<GameObject>();
    [SerializeField] private int DUNGEON_SIZE;
    private NavMeshController NavMeshController;
    private void Start()
    {
        NavMeshController = new NavMeshController();
        CreateDungeon();
    }

    private void CreateDungeon()
    {
        DUNGEON_SIZE = Random.Range(5, 11);
        InitializeDungeon();
        SetDungeonPlaces();
        BakeNavMesh();
        //NavMeshController.BakeNavMesh();
    }
    private void InitializeDungeon()
    {
            
            for(int i = 0; i < 10; i++)
            {
                GameObject tempObj = Instantiate(dungeonPrefabs[0], Vector3.zero, Quaternion.identity);
                dungeonPieces.Add(tempObj);
                tempObj.gameObject.name = ("sajt" + i);
            }
    }

    private void SetDungeonPlaces()
    {
        for (int i = 1; i < dungeonPieces.Count; i++)
        {
            List<GameObject> prevEntranceList = dungeonPieces[i - 1].GetComponentInChildren<DungeonRoomEntrances>().entranceList;
            List<GameObject> thisEntranceList = dungeonPieces[i].GetComponentInChildren<DungeonRoomEntrances>().entranceList;

            int prevTemp = 0;
            DirectionEnum dirTemp = DirectionEnum.W;
            for (int j = 0; j < prevEntranceList.Count; j++)
            {
                if (prevEntranceList[j].GetComponent<Entrance>().quarter == DirectionEnum.N)
                {
                    prevTemp = j;
                    dirTemp = DirectionEnum.N;
                }
                else if (dirTemp != DirectionEnum.N &&
                    prevEntranceList[j].GetComponent<Entrance>().quarter == DirectionEnum.E)
                {
                    prevTemp = j;
                    dirTemp = DirectionEnum.E; 
                }
                else if (dirTemp != DirectionEnum.N &&
                         dirTemp != DirectionEnum.E &&
                         prevEntranceList[j].GetComponent<Entrance>().quarter == DirectionEnum.S)
                {
                    prevTemp = j;
                    dirTemp = DirectionEnum.S;
                }
                else if(dirTemp != DirectionEnum.N &&
                        dirTemp != DirectionEnum.E &&
                        dirTemp != DirectionEnum.S)
                {
                    prevTemp = j;
                    dirTemp = DirectionEnum.W;
                }
            }
            int thisTemp = Random.Range(0, thisEntranceList.Count);
            
            while (thisEntranceList[thisTemp].GetComponent<Entrance>().isConnected)
            {
                thisTemp = Random.Range(0, thisEntranceList.Count);
            }

            while (prevEntranceList[prevTemp].GetComponent<Entrance>().quarter !=
                   thisEntranceList[thisTemp].GetComponent<Entrance>().quarter)
            {
                RotateRoom(dungeonPieces[i]);
            }

            RotateRoomIntoPosition(dungeonPieces[i]);
            
            Vector3 move = prevEntranceList[prevTemp].transform.position - thisEntranceList[thisTemp].transform.position;
            dungeonPieces[i].transform.position += move;

            prevEntranceList[prevTemp].GetComponent<Entrance>().SetConnection();
            thisEntranceList[thisTemp].GetComponent<Entrance>().SetConnection();
            
        }
        
    }

    private void RotateRoom(GameObject room)
    {
        room.transform.Rotate(0, 90,0);
        room.GetComponentInChildren<DungeonRoomEntrances>().RotateQuarters();
    }
    private void RotateRoomIntoPosition(GameObject room)
    {
        for(int i = 0; i < 2; i++)
            RotateRoom(room);
    }

    void BakeNavMesh()
    {
        NavMeshSurface navMeshSurface = dungeonPieces[0].GetComponent<NavMeshSurface>();
        NavMeshController.BakeNavMesh(navMeshSurface);
    }
    
}*/
