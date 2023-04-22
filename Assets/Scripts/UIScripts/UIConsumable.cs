using ObjectScripts.Consumable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class UIConsumable : MonoBehaviour
    {
        [SerializeField] private Image ConsumableImage;
        [SerializeField] private TextMeshProUGUI ConsumableQuantityText;
        
        
        private void Start()
        {
            ConsumableImage.color = Color.clear;
            ConsumableQuantityText.text = "";

            ConsumableController.ConsumableSelectionChangedEvent += DrawConsumables;
            ConsumableController.ConsumeEvent += DrawConsumables;
        }

        private void OnDestroy()
        {
            ConsumableController.ConsumableSelectionChangedEvent -= DrawConsumables;
            ConsumableController.ConsumeEvent -= DrawConsumables;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                IConsumable potion = new HealthPotion();
                ConsumableController.Instance.AddToConsumables(potion, 1);
            }
        }

        private void DrawConsumables()
        {
            ConsumableImage.color = Color.white;
            ConsumableImage.sprite = ConsumableController.Instance.UIGetConsumableSprite();
            ConsumableQuantityText.text = ConsumableController.Instance.UIGetConsumableQuantity().ToString();
        }
    }
}
