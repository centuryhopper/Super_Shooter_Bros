using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

namespace Game.PlayerCharacter
{
    /// <summary>
    /// allows player to check for any ledges to grab onto
    /// </summary>
    public class LedgeChecker : MonoBehaviour
    {
        [HideInInspector]
        public Ledge checkLedge = null, getLedge = null;
        public bool isGrabbingLedge = false;

        void OnTriggerEnter(Collider other)
        {
            checkLedge = other.GetComponent<Ledge>();
            isGrabbingLedge = checkLedge != null;
            getLedge = isGrabbingLedge ? checkLedge : null;
            Debug.Log($"is grabbing ledge: {isGrabbingLedge}");
        }

        void OnTriggerExit(Collider other)
        {
            checkLedge = other.GetComponent<Ledge>();
            if (checkLedge != null)
            {
                isGrabbingLedge = false;
            }
            Debug.Log($"is grabbing ledge now is: {isGrabbingLedge}");
        }
    }
}
