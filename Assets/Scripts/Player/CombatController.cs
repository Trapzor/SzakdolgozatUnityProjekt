using CommonProperties;
using UnityEngine;

namespace Player
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private GameObject weapon;
        [SerializeField] private Weapon actualWeapon;
        [SerializeField] private Animator weaponAnimator;
        [SerializeField] private bool isAttacking;

        private void Awake()
        {
            actualWeapon = weapon.GetComponent<Weapon>();
            weaponAnimator = weapon.GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Attack();
            }
        }

        void Attack()
        {
            if(!actualWeapon.GetIsAttacking())
                weaponAnimator.SetTrigger("AttackTrigger");
        }
    }
}
