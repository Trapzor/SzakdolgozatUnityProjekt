using UnityEngine;

namespace DungeonMaker
{
    public class DMRoom : MonoBehaviour
    {
        [SerializeField] bool isIntersecting;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int count;
        [SerializeField] BoxCollider box;
        [SerializeField] private GameObject CENTER;
        [SerializeField] private GameObject EnemySpawnArea;
        private Collider[] intersecting;

        private void Awake()
        {
            box = GetComponent<BoxCollider>();
            _layerMask = LayerMask.GetMask("Room");
        }

        private void Start()
        {
            intersecting = Physics.OverlapBox(CENTER.transform.position,box.bounds.size / 2, Quaternion.identity, 1<<10);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                GetIsIntersecting();
            
        }

        public bool GetIsIntersecting()
        {
            // var size = box.bounds.size / 2;
            // box.enabled = false;

            Physics.SyncTransforms();
            intersecting = Physics.OverlapBox(CENTER.transform.position,box.bounds.size / 2, Quaternion.identity, 1<<10);
            if (intersecting.Length <= 1)
            {
                isIntersecting = false;
            }
            else
            {
                isIntersecting = true;
            }

            count = intersecting.Length;
            // box.enabled = true;
            return isIntersecting;
        }

        public BoxCollider GetSpawnArea()
        {
            return EnemySpawnArea.GetComponent<BoxCollider>();
        }

        private void OnDrawGizmos()
        {
            // box.enabled = false;
            Gizmos.color = Color.red;
            Gizmos.DrawCube(CENTER.transform.position, box.bounds.size);
            // box.enabled = true;
        }
    }
}
