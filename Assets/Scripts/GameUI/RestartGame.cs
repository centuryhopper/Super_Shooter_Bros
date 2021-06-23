using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.PlayerCharacter;

public class RestartGame : MonoBehaviour
{
    public static bool gameHasEnded = false;
    [SerializeField] GameObject gameOverMenuUI = null;
    [SerializeField] Health playerHealth = null;

    void OnEnable()
    {
        gameOverMenuUI.SetActive(false);
        // Health.restartGameDelegate += gameOverMenu;
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        }
    }

    void Update()
    {
        if (playerHealth.isDead)
        {
            gameOverMenu();
        }
    }

    public void gameOverMenu()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            UnityEngine.Debug.Log($"game over");
            // show game over menu
            gameOverMenuUI.SetActive(true);
        }
    }

    public void restartGame()
    {
        // reset variables for the next time the scene is loaded
        playerHealth.isDead = gameHasEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
