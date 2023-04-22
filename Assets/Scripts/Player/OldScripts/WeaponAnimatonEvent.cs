using UnityEngine;

namespace Player.OldScripts
{
    public class WeaponAnimatonEvent : MonoBehaviour
    {
        private AttackController attacking;

        private void Awake()
        {
            attacking = GetComponentInParent<AttackController>();
        }

        private void EnableAttack()
        {
            attacking.EnableAttack();
        }

        private void DisableAttack()
        {
            attacking.DisableAttack();
        }
    
    }
}
