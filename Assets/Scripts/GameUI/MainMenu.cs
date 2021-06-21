using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // go to next scene
        UnityEngine.Debug.Log($"moving to next scene");
    }

    public void QuitGame()
    {
        UnityEngine.Debug.Log($"Quitting game");
    }
}
