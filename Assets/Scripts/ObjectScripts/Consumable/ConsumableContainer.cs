using UnityEngine;

namespace ObjectScripts.Consumable
{
    public class ConsumableContainer : MonoBehaviour
    {
        public static ConsumableContainer Instance;
        private IConsumable _healthPotion;
        private IConsumable _manaPotion;
        private void Awake()
        {
            Instance = this;

            _healthPotion = new HealthPotion();
            _manaPotion = new ManaPotion();
        }

        public IConsumable healthPotion
        {
            get => _healthPotion;
            set => _healthPotion = value;
        }

        public IConsumable manaPotion
        {
            get => _manaPotion;
            set => _manaPotion = value;
        }
    }
}
