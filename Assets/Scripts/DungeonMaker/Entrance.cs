using UnityEngine;

namespace DungeonMaker
{
    public class Entrance : MonoBehaviour
    {
        /*[SerializeField] private GameObject entranceDirection;
        [SerializeField] internal Vector3 direction;
        [SerializeField] internal DirectionEnum quarter;
        [SerializeField] internal bool isConnected;

        internal void SetDirection()
        {
            direction = Vector3.Normalize(entranceDirection.transform.position - this.gameObject.transform.position);
        }

        internal void SetConnection()
        {
            isConnected = true;
        }*/

        [SerializeField] internal DMEntranceEnum quarter;

        [SerializeField] public bool isConnected = false;

        [SerializeField] private GameObject doorWall;
        [SerializeField] private GameObject solidWall;

        private void Awake()
        {
            if (doorWall == null)
                doorWall = new GameObject();
            if (solidWall == null)
                solidWall = new GameObject();
            isConnected = false;
            solidWall.SetActive(false);
        }

        public bool GetIsConnected()
        {
            return isConnected;
        }

        public void SetConnected()
        {
            isConnected = true;
        }

        public void BreakConnected()
        {
            isConnected = false;
        }

        public void CloseEntrance()
        {
            isConnected = true;
            doorWall.SetActive(false);
            solidWall.SetActive(true);
        }

        public void OpenEntrance()
        {
            if (isConnected)
            {
                doorWall.SetActive(true);
                solidWall.SetActive(false);
            }
        }

    }
}
