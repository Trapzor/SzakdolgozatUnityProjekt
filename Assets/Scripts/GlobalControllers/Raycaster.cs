using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlobalControllers
{
    public class Raycaster : MonoBehaviour
    {
        private Camera mainCamera;
        [SerializeField] private InputAction leftClickAction;

        public static Action<RaycastHit> OnLeftClick;
    
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            leftClickAction.Enable();
            leftClickAction.performed += Raycasting;
        
        }

        private void OnDisable()
        {
            leftClickAction.performed -= Raycasting;
            leftClickAction.Disable();
        }

        public void Raycasting(InputAction.CallbackContext context)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~(1 << 8)) /*&& hit.collider*/)
            {
                OnLeftClick.Invoke(hit);
            }
        }    

    }
}
