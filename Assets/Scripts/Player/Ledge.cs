using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

namespace Game.PlayerCharacter
{
    public class Ledge : MonoBehaviour
    {
        [SerializeField] Vector3 offset;
        public Vector3 Offset { get { return offset; } }
    }
}
