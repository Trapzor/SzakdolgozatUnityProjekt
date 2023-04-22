using System;
using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        public static PlayerCharacter Instance;
        public static event Action PlayerHealthChangedEvent;
        public static event Action PlayerManaChangedEvent;
        public static event Action PlayerDeathEvent;
        
        [SerializeField] 
        private float maxHealth;
        [SerializeField]
        private float health;

        [SerializeField] private float maxMana;
        [SerializeField] private float mana;
        
        
        [SerializeField]
        private float base_dmg;
        [SerializeField]
        private float base_speed;

        private bool _isDead;

        private void Awake()
        {
            health = maxHealth;
            Instance = this;
            _isDead = false;
        }

        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }

        public float Health
        {
            get => health;
            set => health = value;
        }

        public float MaxMana
        {
            get => maxMana;
            set => maxMana = value;
        }

        public float Mana
        {
            get => mana;
            set => mana = value;
        }


        public float BaseDmg
        {
            get => base_dmg;
            set => base_dmg = value;
        }
        public float BaseSpeed
        {
            get => base_speed;
            set => base_speed = value;
        }

        public void Heal(float healingValue)
        {
            health += healingValue;
            if (health >= maxHealth)
                health = maxHealth;
            PlayerHealthChangedEvent?.Invoke();

        }

        public void ManaHeal(float value)
        {
            mana += value;
            if (mana >= maxMana)
                mana = maxMana;
            PlayerManaChangedEvent?.Invoke();
        }
        
        public void GetDamaged(float damageValue)
        {
            health -= damageValue;
            PlayerHealthChangedEvent?.Invoke();
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (!_isDead)
            {
                PlayerDeathEvent?.Invoke();
                _isDead = true;
            }
        }
    }
}
