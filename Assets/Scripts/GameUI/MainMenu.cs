using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameUI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            // go to next scene
            UnityEngine.Debug.Log($"moving to next scene");
        }

        public void quitGame()
        {
            UnityEngine.Debug.Log($"Quitting game");
        }
    }
}
