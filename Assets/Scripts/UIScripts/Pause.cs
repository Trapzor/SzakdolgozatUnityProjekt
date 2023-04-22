using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIScripts
{
    public class Pause : MonoBehaviour
    {
        public static event Action GamePausedEvent;
        public static event Action GameResumedEvent;
        
        [SerializeField]
        private Canvas canvas;

        private void Awake()
        {
            canvas.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu();
            }
        }

        private void PauseMenu()
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            canvas.enabled = true;
            GamePausedEvent?.Invoke();
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            canvas.enabled = false;
            GameResumedEvent?.Invoke();
        }

        public void BUTTOMResume()
        {
            ResumeGame();
        }

        public void BUTTONExit()
        {
            SceneManager.LoadScene("Main Menu");
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("SampleScene");
        }
    }
}
