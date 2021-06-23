using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameUI
{
    public class GamePauseMenu : MonoBehaviour
    {
        [HideInInspector]
        public static bool isGamePaused = false;
        [SerializeField] GameObject pauseMenuUI = null;

        void Start()
        {
            pauseMenuUI.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused) resume();
                else pause();
            }
        }

        void pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isGamePaused = true;
        }

        public void resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isGamePaused = false;
        }

    }
}
