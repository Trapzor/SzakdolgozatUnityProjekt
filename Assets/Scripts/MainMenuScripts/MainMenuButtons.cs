using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuScripts
{
    public class MainMenuButtons : MonoBehaviour
    {
        public void BUTTONPlay()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void BUTTONExit()
        {
            Application.Quit();
        }
    }
}
