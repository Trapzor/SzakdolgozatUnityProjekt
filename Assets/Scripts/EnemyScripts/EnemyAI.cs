using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent; 
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 startingPosition;

        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask playerLayer;

        [SerializeField] private float walkPointRange;
        //private Vector3 walkPoint;

        [SerializeField] private float sightRange;
        [SerializeField] private bool playerInSightRange;

        [SerializeField] private float attackRange;
        [SerializeField] private bool playerInAttackRange;

        [SerializeField]
        private bool isAttacking;
        
        private Enemy _enemy;
        private Animator _enemyAnimator;
        private EnemyAnimationController _animationController;

        [SerializeField] private EnemyWeapon weapon;

        private void Awake()
        {
            isAttacking = false;
            playerInAttackRange = false;
            playerInSightRange = false;
            
            _enemy = GetComponent<Enemy>();
            _enemyAnimator = _enemy.GetComponent<Animator>();
            _animationController = EnemyAnimationController.GetInstance();
            
            groundLayer = LayerMask.GetMask("Traversable");
            playerLayer = LayerMask.GetMask("Player");
            navMeshAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            startingPosition = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
            Idle();
        }
        private void FixedUpdate()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

            if (!isAttacking)
            {
                if (playerInSightRange && !playerInAttackRange)
                {
                    ChasePlayer();
                    navMeshAgent.isStopped = false;
                }
                else if (playerInSightRange && playerInAttackRange)
                {
                    gameObject.transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position); 
                    if (!isAttacking)
                    {
                        StartCoroutine(AttackPlayer());
                    }
                }
                else
                {
                    ReturnAndWait();
                }
            }
        }

        private IEnumerator AttackPlayer()
        {
            _animationController.PlayAttack(_enemyAnimator);

            yield return null;
        }
        
        private void ReturnAndWait()
        {
            if (startingPosition.x == transform.position.x && startingPosition.z == startingPosition.z)
                Idle();
            else
            {
                navMeshAgent.SetDestination(startingPosition);
                _animationController.PlayRun(_enemyAnimator);
            }
        }
        private void ChasePlayer()
        {
            navMeshAgent.SetDestination(player.transform.position);
            _animationController.PlayRun(_enemyAnimator);
        }

        private void Idle()
        {
            _animationController.PlayIdle(_enemyAnimator);
        }

        internal void Die()
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            weapon.gameObject.SetActive(false);
            _animationController.PlayDeath(_enemyAnimator);

            enabled = false;
        }

        void ANIMATOR_SET_ATTACKTO_FALSE()
        {
            weapon.GetComponent<BoxCollider>().enabled = false;
            isAttacking = false;
        }

        void ANIMATOR_SETATTACK_TO_TRUE()
        {
            weapon.GetComponent<BoxCollider>().enabled = true;
            isAttacking = true;
        }

        internal void SetStartingPosition(Vector3 s)
        {
            startingPosition = s;
        }

        internal Vector3 GetStartingPosition()
        {
            return startingPosition;
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.DrawSphere(transform.position, sightRange);
        // }
    }
}
