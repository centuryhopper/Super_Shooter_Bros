using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.portal_manager;


public class TriggeredPortal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PortalManager.instance.loadDesiredScene();
    }
}
