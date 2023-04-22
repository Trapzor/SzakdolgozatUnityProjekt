using UnityEngine;

namespace Player
{
    public class PlayerMovementKB : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed;
        private Transform _camera;
        
        private void Awake()
        {
            _camera = Camera.main.transform;
            movementSpeed = GetComponent<PlayerCharacter>().BaseSpeed;
        }

        private void Update()
        {
            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            
            Vector3 camF = _camera.forward;
            Vector3 camR = _camera.right;

            camF.y = 0;
            camR.y = 0;

            camF = camF.normalized;
            camR = camR.normalized;

            Vector3 nextPosition = (camF * dir.z + camR * dir.x).normalized * movementSpeed;

            transform.position += nextPosition * Time.deltaTime;
        }
    }
}
