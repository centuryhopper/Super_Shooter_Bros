using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.portal_manager;
using Game.Spawner;
using Game.EnemyAI;
using TMPro;

namespace Game.LevelDesign
{
    public class TriggeredPortal : MonoBehaviour
    {
        private Coroutine showAlertRoutine = null;
        [SerializeField] float showAlertMsgTime = 5f;
        private WaitForSeconds wait;
        [SerializeField] GameObject alertMsg = null;

        void Start()
        {
            wait = new WaitForSeconds(showAlertMsgTime);
            alertMsg.SetActive(false);
        }

        void OnTriggerEnter(Collider other)
        {
            // only call this when all enemies in the scene are deactivated
            if (Enemy.enemyDeaths == EnemySpawner.totalNumEnemies)
            {
                PortalManager.instance.loadDesiredScene();
            }
            else
            {
                // show alert message
                if (showAlertRoutine != null) StopCoroutine(showAlertRoutine);
                showAlertRoutine = StartCoroutine(showAlert());
            }
        }

        private IEnumerator showAlert()
        {
            alertMsg.SetActive(true);
            yield return wait;
            alertMsg.SetActive(false);
        }



    }
}
