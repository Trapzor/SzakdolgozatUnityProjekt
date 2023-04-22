using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ObjectScripts.StairsToNextLevel
{
    public class Stairs : MonoBehaviour, Interactable
    {
        
        [SerializeField] private GameObject interactablePrompt;
        private MeshRenderer _prompt;

        private void Start()
        {
            _prompt = interactablePrompt.GetComponent<MeshRenderer>();
            _prompt.enabled = false;
        }

        public void ShowPrompt()
        {
            _prompt.enabled = true;
        }

        public void HidePrompt()
        {
            _prompt.enabled = false;
        }

        public void Interact()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
