using Player;
using UnityEngine;

namespace ObjectScripts.Consumable
{
    public class HealthPotion : AConsumable
    {
        private int healingPotency;
        private Sprite _sprite;

        public HealthPotion()
        {
            healingPotency = 50;
            _sprite = Resources.Load<Sprite>("Sprites/HealthPotion");
        }
        
        public override void Consume()
        {
            PlayerCharacter.Instance.Heal(healingPotency);
        }

        public override Sprite GetSprite()
        {
            return _sprite;
        }
    }
}
