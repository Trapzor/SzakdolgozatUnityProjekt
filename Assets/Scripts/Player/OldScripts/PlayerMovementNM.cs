using System.Collections;
using GlobalControllers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Player.OldScripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Raycaster))]
    public class PlayerMovementNM : MonoBehaviour
    {
        private Coroutine coroutine;
        private Vector3 targetPosition;

        private NavMeshAgent navMeshAgent;

        private int traversableLayer;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            traversableLayer = LayerMask.NameToLayer("Traversable");
        }
        private void OnEnable()
        {
            Raycaster.OnLeftClick += Move;
        }
        private void OnDisable()
        {
            Raycaster.OnLeftClick -= Move;
        }

        private void Move(RaycastHit hit)
        {
            if (IsTraversable(hit))
            {
                if(coroutine != null)
                    StopCoroutine(coroutine);
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
                navMeshAgent.destination = target;
                yield return null;
            }
        }

        private bool IsTraversable(RaycastHit hit)
        {
            if (hit.collider.gameObject.layer.CompareTo(traversableLayer) == 0)
                return true;
            return false;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(targetPosition, 0.5f);
        }
    }
}
