using System.Collections;
using System.Collections.Generic;
using Game.HealthManager;
using UnityEngine;


namespace Game.EnemyAI
{
    /// <summary>
    /// a component on the ai animated model for calling animation events
    /// </summary>
    public class ProvideDamage : MonoBehaviour
    {
        // jab animation event (needs monobehaviour to work)
        public void damagePlayer()
        {
            HealthDamageManager.instance.Jab();
        }
    }
}
