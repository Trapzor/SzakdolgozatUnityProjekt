using Player;
using UnityEngine;

namespace ObjectScripts.Consumable
{
    public class ManaPotion : AConsumable
    {
        private int potency;
        private Sprite _sprite;

        public ManaPotion()
        {
            potency = 10;
            _sprite = Resources.Load<Sprite>("Sprites/ManaPotion");
        }


        public override void Consume()
        {
            PlayerCharacter.Instance.ManaHeal(potency);
        }

        public override Sprite GetSprite()
        {
            return _sprite;
        }
    }
}
