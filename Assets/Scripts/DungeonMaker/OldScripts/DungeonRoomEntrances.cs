/*using System.Collections.Generic;
using DungeonMaker;
using UnityEngine;

public class DungeonRoomEntrances : MonoBehaviour
{
    [SerializeField] internal List<GameObject> entranceList = new List<GameObject>();

    internal void InitializeDirections()
    {
        foreach (GameObject g in entranceList)
        {
            g.GetComponentInChildren<Entrance>().SetDirection();
        }
    }

    internal void RotateQuarters()
    {
        foreach (GameObject g in entranceList)
        {
            Entrance entrance = g.GetComponent<Entrance>();
            
            if (entrance.quarter == DirectionEnum.N)
                entrance.quarter = DirectionEnum.E;
            else if (entrance.quarter == DirectionEnum.E)
                entrance.quarter = DirectionEnum.S;
            else if (entrance.quarter == DirectionEnum.S)
                entrance.quarter = DirectionEnum.W;
            else if (entrance.quarter == DirectionEnum.W)
                entrance.quarter = DirectionEnum.N;
        }
    }
}*/
