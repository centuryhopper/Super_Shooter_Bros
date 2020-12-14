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
        Ledge anyLedge = null;
        public bool isGrabbingLedge;

        void OnTriggerEnter(Collider other)
        {
            anyLedge = other.GetComponent<Ledge>();
            isGrabbingLedge = anyLedge != null;
            Debug.Log($"is grabbing ledge: {isGrabbingLedge}");
        }

        void OnTriggerExit(Collider other)
        {
            anyLedge = other.GetComponent<Ledge>();
            if (anyLedge != null)
            {
                isGrabbingLedge = false;
            }
            Debug.Log($"is grabbing ledge now is: {isGrabbingLedge}");
        }
    }
}
