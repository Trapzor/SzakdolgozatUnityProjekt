using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class UIMana : MonoBehaviour
    {
        [SerializeField] private Slider manaSlider;

        private void Start()
        {
            manaSlider.minValue = 0f;
            manaSlider.maxValue = PlayerCharacter.Instance.MaxMana;
            manaSlider.value = PlayerCharacter.Instance.Mana;

            PlayerCharacter.PlayerManaChangedEvent += UpdateManaChangedUI;
        }

        private void OnDestroy()
        {
            PlayerCharacter.PlayerManaChangedEvent -= UpdateManaChangedUI;
        }

        private void UpdateManaChangedUI()
        {
            manaSlider.value = PlayerCharacter.Instance.Mana;
        }
    }
}
