using System;
using TMPro;
using UnityEngine;
using Toggle = UnityEngine.UI.Toggle;

namespace UIScripts
{
    public class Options : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;

        [SerializeField] private TMP_Dropdown dropdown;

        private int _screenWidth;
        private int _screenHeight;

        public void BUTTONApply()
        {
            String[] resolution = dropdown.captionText.text.Split("x");
            _screenWidth = Int32.Parse(resolution[0]);
            _screenHeight = Int32.Parse(resolution[1]);
        
            Screen.SetResolution(_screenWidth, _screenHeight, toggle.isOn);
        }
    }
}
