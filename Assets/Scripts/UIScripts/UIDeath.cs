using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class UIDeath : MonoBehaviour
    {
        [SerializeField]
        private Canvas deathCanvas;

        private void Start()
        {
            deathCanvas.enabled = false;
            PlayerCharacter.PlayerDeathEvent += ShowGameOverScreen;
        }

        private void OnDestroy()
        {
            PlayerCharacter.PlayerDeathEvent -= ShowGameOverScreen;
        }

        private void ShowGameOverScreen()
        {
            Time.timeScale = 0;
            deathCanvas.enabled = true;
        }

        public void BUTTONReturnToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("SampleScene");
        }
    }
}
