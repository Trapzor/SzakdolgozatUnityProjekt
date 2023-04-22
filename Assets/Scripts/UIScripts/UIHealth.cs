using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class UIHealth : MonoBehaviour
    {

        [SerializeField] private Slider healthSlider;
        private void Start()
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = PlayerCharacter.Instance.MaxHealth;
            healthSlider.value = healthSlider.maxValue;

            PlayerCharacter.PlayerHealthChangedEvent += UpdateHealthChangedUI;
        }

        private void OnDestroy()
        {
            PlayerCharacter.PlayerHealthChangedEvent -= UpdateHealthChangedUI;
        }

        private void UpdateHealthChangedUI()
        {
            healthSlider.value = PlayerCharacter.Instance.Health;
        }


    }
}
