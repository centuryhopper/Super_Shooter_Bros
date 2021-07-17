using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void quitGame()
    {
        UnityEngine.Debug.Log($"quitting game");
        Application.Quit();
    }
}
