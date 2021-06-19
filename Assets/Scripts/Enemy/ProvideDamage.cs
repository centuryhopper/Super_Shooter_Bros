using System.Collections;
using System.Collections.Generic;
using Game.HealthManager;
using UnityEngine;


public class ProvideDamage : MonoBehaviour
{
    // jab animation event (needs monobehaviour to work)
    public void damagePlayer()
    {
        HealthDamageManager.Instance.Jab();
    }
}
