using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName = null;

    public void loadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        UnityEngine.Debug.Log($"Quitting game");
    }
}
