using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameUI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] string sceneName = "MainMenu";
        public void loadMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }
    }
}
