using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonMaker
{
    public class DMEntrances : MonoBehaviour
    {
        [SerializeField] private Entrance mainC1;
        [SerializeField] private Entrance mainC2;
        
        [SerializeField] private List<Entrance> entrances;

        public Entrance GetFreeEntrance()
        {
            for(int i = 0; i < entrances.Count; i++)
                if (!entrances[i].GetIsConnected())
                    return entrances[i];
            return null;
        }

        public List<Entrance> GetAllEntrances()
        {
            return entrances;
        }

        public List<Entrance> GetAllFreeEntrances()
        {
            List<Entrance> temp = new List<Entrance>();
            for(int i = 0; i < entrances.Count; i++)
                if (!entrances[i].GetIsConnected())
                    temp.Add(entrances[i]);

            return temp;
        }

        public void SetMainConnection(Entrance e1, Entrance e2)
        {
            mainC1 = e1;
            mainC2 = e2;
            
            mainC1.SetConnected();
            mainC2.SetConnected();
        }

        public void BreakMainConnection()
        {
            if (mainC1.isConnected)
                mainC1.BreakConnected();
            
            if(mainC2.isConnected)
                mainC2.BreakConnected();
        }
        public void RotateQuarters()
        {
            for (int i = 0; i < entrances.Count; i++)
            {
                if (entrances[i].quarter == DMEntranceEnum.N)
                    entrances[i].quarter = DMEntranceEnum.E;
                else if (entrances[i].quarter == DMEntranceEnum.E)
                    entrances[i].quarter = DMEntranceEnum.S;
                else if (entrances[i].quarter == DMEntranceEnum.S)
                    entrances[i].quarter = DMEntranceEnum.W;
                else
                    entrances[i].quarter = DMEntranceEnum.N;
            }
            
            // foreach (Entrance e in entrances)
            // {
            //     if (e.quarter == DMEntranceEnum.N)
            //         e.quarter = DMEntranceEnum.E;
            //     else if (e.quarter == DMEntranceEnum.E)
            //         e.quarter = DMEntranceEnum.S;
            //     else if (e.quarter == DMEntranceEnum.S)
            //         e.quarter = DMEntranceEnum.W;
            //     else
            //         e.quarter = DMEntranceEnum.N;
            // }
        }


    }
}
