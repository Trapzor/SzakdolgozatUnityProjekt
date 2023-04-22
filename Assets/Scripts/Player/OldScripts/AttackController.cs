using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.OldScripts
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private InputAction rightClickAction;

        [SerializeField] private float attackRange;
        [SerializeField] private GameObject weapon;

        [SerializeField] private bool isAttacking = false;

        [SerializeField] private Animator weaponAnimation;
        //TODO: Refactor this into separate script.

        [SerializeField] private float cooldownTime = 1;
    
        private void Awake()
        {
            weaponAnimation = weapon.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            rightClickAction.Enable();
            rightClickAction.performed += Attack;
        }

        private void OnDisable()
        {
            rightClickAction.performed -= Attack;
            rightClickAction.Disable();
        }

        private void Attack(InputAction.CallbackContext context)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                weaponAnimation.SetTrigger("Attack");
                /*float animationlength = weaponAnimation.GetCurrentAnimatorStateInfo(0).length;
            Debug.Log(animationlength.ToString());
            StartCoroutine(WaitForAttackToFinish(animationlength));*/
                //StartCoroutine(WaitForAttackToFinish2());
            }
        }

        /*private IEnumerator WaitForAttackToFinish2()
    {
        Debug.Log(AnimationIsPlaying());
        yield return new WaitUntil(AnimationIsPlaying);
        isAttacking = false;
    }

    private bool AnimationIsPlaying()
    {
        if (weaponAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Attack")
            //weaponAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            )
            return true;
        return false;

    }

    private bool isIdle()
    {
        Debug.Log(weaponAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Idle"));
        return weaponAnimation.GetCurrentAnimatorStateInfo(0).IsTag("Idle");
    }
    
    private IEnumerator WaitForAttackToFinish(float sec)
    {
        yield return new WaitForSeconds(sec);
        isAttacking = false;
    }*/

        public void EnableAttack()
        {
            isAttacking = false;
        }

        public void DisableAttack()
        {
            isAttacking = true;
        }


    }
}
