using Player;
using UnityEngine;

namespace EnemyScripts
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] private float damage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerCharacter>())
                other.gameObject.GetComponent<PlayerCharacter>().GetDamaged(damage);
        }
    }
}
