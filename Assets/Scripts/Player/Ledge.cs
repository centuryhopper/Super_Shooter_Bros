using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace Game.PlayerCharacter
{
    public class Ledge : MonoBehaviour
    {
        [SerializeField] Vector3 endPosition;
        public Vector3 EndPosition { get { return endPosition; } }

        [Tooltip("The vector offset from the center point of this ledge")]
        [SerializeField] Vector3 offset;

        public Vector3 Offset { get { return offset; } }

        [SerializeField] float customRadius;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.localPosition, customRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.localPosition + Offset, customRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.localPosition + endPosition, customRadius);
        }
    }
}
