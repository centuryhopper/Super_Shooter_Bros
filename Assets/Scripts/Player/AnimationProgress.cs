using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    /// <summary>
    /// keeps a custom record of the data that flows thru the animation
    /// and will serve as the middle man between animation states data and the player movement script
    /// </summary>
    public class AnimationProgress : MonoBehaviour
    {

        [Header("Update Box Collider")]
        public bool isUpdatingBoxCollider;
        public bool isUpdatingSpheres;
        public Vector3 targetSize;
        public float sizeSpeed;
        public Vector3 targetCenter;
        public float centerSpeed;

    }
}
