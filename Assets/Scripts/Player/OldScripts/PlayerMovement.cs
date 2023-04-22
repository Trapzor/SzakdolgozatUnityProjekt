using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.OldScripts
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private InputAction leftClickAction;
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float rotationSpeed = 3f;

        private Camera mainCamera;
        private Coroutine coroutine;
        private Vector3 targetPosition;

        private CharacterController characterController;

        private void Awake()
        {
            mainCamera = Camera.main;
            characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            leftClickAction.Enable();
            leftClickAction.performed += Move;
        }

        private void OnDisable()
        {
            leftClickAction.performed -= Move;
            leftClickAction.Disable();
        }

        private void Move(InputAction.CallbackContext context)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = StartCoroutine(PlayerMoveTowards(hit.point));
                targetPosition = hit.point;
            }
        }

        private IEnumerator PlayerMoveTowards(Vector3 target)
        {
            float playerDistanceToFloor = transform.position.y - target.y;
            target.y += playerDistanceToFloor;
        
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                Vector3 direction = target - transform.position;
                Vector3 movement = direction.normalized * (movementSpeed * Time.deltaTime);
                characterController.Move(movement);
                
                rotatePlayerTowards(direction);
                
                

                yield return null;
            }
        }
        
        private void rotatePlayerTowards(Vector3 direction)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction.normalized),
                rotationSpeed * Time.deltaTime);

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(targetPosition, 0.1f);
        }
    }
}

