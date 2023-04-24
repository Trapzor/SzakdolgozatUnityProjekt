using System.Collections;
using UnityEngine;

namespace TestableScripts
{
    public class TestableEnemy : MonoBehaviour
    {
        private bool _isAlive;

        private void Awake()
        {
            _isAlive = true;
        }

        public IEnumerator Die()
        {
            yield return new WaitForSeconds(3);
            _isAlive = false;
        }

        public bool GetIsAlive()
        {
            return _isAlive;
        }
    }
}