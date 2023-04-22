using UnityEngine;

namespace ObjectScripts.Consumable
{ 
    public interface IConsumable
    {
        public void Consume();
        public Sprite GetSprite();
    }
}
