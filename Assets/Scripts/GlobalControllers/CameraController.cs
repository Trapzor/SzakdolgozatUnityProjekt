using UnityEngine;

namespace GlobalControllers
{
    public class CameraController : MonoBehaviour
    {
        private GameObject mainCamera;
        [SerializeField] private GameObject player;
        [SerializeField]
        private Vector3 offset;

        private Transform cameraTrans;
        private Transform playerTrans;
    
        void Start()
        {
            mainCamera = gameObject;
        
            cameraTrans = mainCamera.transform;
            playerTrans = player.transform;

            CenterPlayer();
        

        }

        private void LateUpdate()
        {
            FollowPlayer();
        }

        void CenterPlayer()
        {
            Vector3 position = playerTrans.position + offset;
            cameraTrans.position = position;
        }

        void FollowPlayer()
        {
            Vector3 position = player.transform.position + offset;
            mainCamera.transform.position = position;
        }
    }
}
