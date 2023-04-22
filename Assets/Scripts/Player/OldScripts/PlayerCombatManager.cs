using CommonProperties;
using UnityEngine;

namespace Player.OldScripts
{
    public class PlayerCombatManager : MonoBehaviour
    {
        //[SerializeField] private InputAction leftClickAction;
        [SerializeField] private GameObject target;
        [SerializeField] private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }
        private void OnEnable()
        {
            
            //leftClickAction.Enable();
            //leftClickAction.performed += Targeting;
        }
        private void OnDisable()
        {
            //leftClickAction.performed -= Targeting;
            //leftClickAction.Disable();
        }

        private void Targeting(RaycastHit hit)
        {
            /*Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider)
            {
                if (hit.transform.GetComponent<Targetable>())
                    target = hit.transform.gameObject;
                else
                {
                    target = null;
                }
            }*/
            if (hit.transform.GetComponent<Targetable>())
                target = hit.transform.gameObject;
            else
            {
                target = null;
            }
        }
    
    }
}
