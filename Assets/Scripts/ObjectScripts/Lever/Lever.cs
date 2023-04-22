using ObjectScripts.Door;
using UnityEngine;

namespace ObjectScripts.Lever
{
    public class Lever : MonoBehaviour, Interactable
    {
        [SerializeField] private GameObject door;
        [SerializeField] private GameObject interactablePrompt;
        private MeshRenderer _prompt;
        
        private Animator _animator;
        private OpenableDoor _openableDoor;
        

        private void Start()
        {
            _prompt = interactablePrompt.GetComponent<MeshRenderer>();
            _openableDoor = door.GetComponent<OpenableDoor>();
            _animator = GetComponent<Animator>();

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
            _animator.SetTrigger("UseLeverTrigger");
            if (_openableDoor.GetIsOpened())
            {
                _openableDoor.CloseDoor();
            }
            else
            {
                _openableDoor.OpenDoor();
            }
        }
    }
}
