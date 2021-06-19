using System.Collections;
using System.Collections.Generic;
using Game.HealthManager;
using UnityEngine;


public class ProvideDamage : MonoBehaviour
{
    public void damagePlayer()
    {
        HealthDamageManager.Instance.Jab();
    }
}
