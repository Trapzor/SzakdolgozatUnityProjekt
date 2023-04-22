using EnemyScripts;
using Player;
using UnityEngine;

namespace CommonProperties
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private float damage;

        [SerializeField]
        private bool isAttacking;

        private void Awake()
        {
            isAttacking = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        public void ChangeIsAttacking()
        {
            if (isAttacking)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                isAttacking = false;
            }

            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
                isAttacking = true;
            }
            
        }

        public bool GetIsAttacking()
        {
            return isAttacking;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<Enemy>())
            {
                Debug.Log(other.gameObject.GetComponent<Enemy>().hp.ToString());
                other.gameObject.GetComponent<Enemy>().GetDamaged(damage + PlayerCharacter.Instance.BaseDmg); 
            }
        }
    }
}
