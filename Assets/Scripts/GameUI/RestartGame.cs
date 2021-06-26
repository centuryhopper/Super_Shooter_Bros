using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.HealthManager;

public class RestartGame : MonoBehaviour
{
    public static bool gameHasEnded = false;
    [SerializeField] GameObject gameOverMenuUI = null;
    [SerializeField] bool shouldDelayShowingMenu = true;
    [SerializeField] float delayTime = 5f;
    WaitForSeconds delay;
    Coroutine showGameOverMenu = null;

    void Start()
    {
        UnityEngine.Debug.Log($"gameHasEnded: {gameHasEnded}");
        gameOverMenuUI.SetActive(false);
        delay = new WaitForSeconds(delayTime);

        // stop the previous coroutine from running if one exists
        if (showGameOverMenu != null)
        {
            StopCoroutine(showGameOverMenu);
        }

    }

    void Update()
    {
        if (HealthDamageManager.instance.isPlayerDead && !gameHasEnded)
        {
            gameHasEnded = true;
            // include a short delay before showing the game over menu
            showGameOverMenu = StartCoroutine(gameOverMenu());
        }
    }

    IEnumerator gameOverMenu()
    {
        yield return delay;
        UnityEngine.Debug.Log($"game over");
        // show game over menu
        gameOverMenuUI.SetActive(true);
    }

    public void restartGame()
    {
        // reset variables for the next time the scene is loaded
        gameHasEnded = false;
        HealthDamageManager.instance.resetPlayerDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
