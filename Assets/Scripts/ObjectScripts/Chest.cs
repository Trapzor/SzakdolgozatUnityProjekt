using System.Collections.Generic;
using ObjectScripts.Consumable;
using UnityEngine;

namespace ObjectScripts
{
    public class Chest : MonoBehaviour, Interactable
    {
        [SerializeField] private GameObject interactablePrompt;
        
        private MeshRenderer prompt;
        private List<IConsumable> contents = new();

        private bool isOpened;
        private Animator animator;
        private void Awake()
        {
            contents.Add(ConsumableContainer.Instance.healthPotion);
            contents.Add(ConsumableContainer.Instance.manaPotion);
            
            animator = GetComponent<Animator>();
            prompt = interactablePrompt.GetComponent<MeshRenderer>();
            prompt.enabled = false;
            isOpened = false;
        }

        public void ShowPrompt()
        {
            if(!isOpened)
                prompt.enabled = true;
        }

        public void HidePrompt()
        {
            prompt.enabled = false;
        }

        public void Interact()
        {
            if (!isOpened)
            {
                OpenChest();
                //TODO
                SetOpened();
                StopAnimation();
                HidePrompt();
            }
        }

        void OpenChest()
        {
            animator.SetTrigger("OpenTrigger");
            foreach (IConsumable item in contents )
            {
                ConsumableController.Instance.AddToConsumables(item, 1);
            }
        }

        void SetOpened()
        {
            isOpened = true;
        }

        void StopAnimation()
        {
            animator.StopPlayback();
        }
    }
}
