using UnityEngine;

namespace EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public float hp;
        private EnemyAI _enemyBehaviour;

        private void Awake()
        {
            _enemyBehaviour = GetComponent<EnemyAI>();
        }

        public void GetDamaged(float damage)
        {
            hp -= damage;
            if (hp <= 0)
                _enemyBehaviour.Die();
        }
    }
}
