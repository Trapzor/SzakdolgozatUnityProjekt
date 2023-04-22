using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace ObjectScripts.Consumable
{
    public class ConsumableController : MonoBehaviour
    {
        public static event Action ConsumableSelectionChangedEvent;
        public static event Action ConsumeEvent;

        public static ConsumableController Instance;
        private ConsumableContainer _consumableContainer;
    
        private int _consumableIndex;
        private List<Tuple<IConsumable, int>> _consumables;

        private Dictionary<IConsumable, int> consumableDict;
        private IConsumable consumeThis;

        private void Awake()
        {
            Instance = this;

            consumableDict = new Dictionary<IConsumable, int>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
                Consume();
        
            if(Input.GetKeyDown(KeyCode.Alpha1))
                DecreaseIndex();
        
            if(Input.GetKeyDown(KeyCode.Alpha3))
                IncreaseIndex();
        }

        void Consume()
        {
            //_consumables[_consumableIndex].Item1.Consume();
            //_consumables[_consumableIndex].Item2 -= 1;

            if (consumableDict[consumeThis] > 0)
            {
                consumeThis.Consume();
                consumableDict[consumeThis] -= 1;
                ConsumeEvent?.Invoke();
            }
        }

        public void AddToConsumables(IConsumable consumable, int quantity)
        {
            //Ha nem Equalst használ a Dictionary akkor ezt kell módosítani!!!! HA SZAR!!!!!!
            if (consumableDict.ContainsKey(consumable))
            {
                consumableDict[consumable] += quantity;
            }
            else
            {
                consumableDict.Add(consumable, quantity);
                if (consumableDict.Count == 1)
                    consumeThis = consumable;
            }
            ConsumableSelectionChangedEvent?.Invoke();
        
            // if (_consumables.Contains(consumable))
            // {
            //     int index = _consumables.IndexOf(consumable);
            //     _consumables[index].AddToQuantity(quantity);
            // }
            // else
            // {
            //     _consumables.Add(consumable);
            //     consumable.AddToQuantity(quantity);
            //     _consumableIndex = _consumables.IndexOf(consumable);
            //     ConsumableSelectionChangedEvent?.Invoke();
            // }
        }
    
        private void IncreaseIndex()
        {
            if (consumableDict != null)
            {
                _consumableIndex = (_consumableIndex + 1) % consumableDict.Count;

                // if (_consumableIndex == _consumables.Count - 1)
                //     _consumableIndex = 0;
                // else
                //     _consumableIndex += 1;

                SetConsumable();

                ConsumableSelectionChangedEvent?.Invoke();
            }
        }

        private void DecreaseIndex()
        {
            if (consumableDict != null)
            {
                _consumableIndex = (_consumableIndex - 1) % consumableDict.Count;

                SetConsumable();

                ConsumableSelectionChangedEvent?.Invoke();
            }
        }
    
        private void SetConsumable()
        {
            int count = 0;
            foreach (KeyValuePair<IConsumable, int> pair in consumableDict)
            {
                if (count == _consumableIndex)
                {
                    consumeThis = pair.Key;
                    break;
                }

                count++;
            }
        }

        public int UIGetConsumableQuantity()
        {
            return consumableDict[consumeThis];
        }

        public Sprite UIGetConsumableSprite()
        {
            return consumeThis.GetSprite();
        }
    }
}
