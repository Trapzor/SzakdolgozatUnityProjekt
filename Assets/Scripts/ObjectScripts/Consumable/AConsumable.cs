using UnityEngine;

namespace ObjectScripts.Consumable
{
    public abstract class AConsumable : IConsumable
    {
        public abstract void Consume();
        public abstract Sprite GetSprite();
    
        public override bool Equals(object obj)
        {
            if(obj is IConsumable)
                return this.GetType() == (obj as IConsumable).GetType();
            return false;
        }

    }
}
