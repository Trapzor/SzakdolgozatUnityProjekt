using UnityEngine;

namespace ObjectScripts.Door
{
    public class OpenableDoor : MonoBehaviour
    {
        private Animator _animator;
        private bool _isOpened;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _isOpened = false;
        }

        public void OpenDoor()
        {
            _animator.SetTrigger("DoorOpenTrigger");
            _isOpened = true;
        }

        public void CloseDoor()
        {
            _animator.SetTrigger("DoorCloseTrigger");
            _isOpened = false;
        }

        public bool GetIsOpened()
        {
            return _isOpened;
        }
    }
}
