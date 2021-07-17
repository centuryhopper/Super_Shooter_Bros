using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.portal_manager
{
    public class PortalManager : MonoBehaviour
    {
        public static PortalManager instance;
        [SerializeField] string sceneName = "MainMenu";

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void loadDesiredScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }
    }
}
