using System.Collections.Generic;
using UnityEngine;
using Game.singleton;
using System;
using Game.Enums;

namespace Game.Hash
{
    /// <summary>
    /// Used to optimize performance on getting and setting animator states by an id instead of
    /// a string
    /// </summary>
    public class HashManager : Singleton<HashManager>
    {
        /// <summary>
        /// Populated with key-value pairs of an
        /// enum in 'AnimationParameters' being the key
        /// and a hashed integer being the value
        /// </summary>
        public Dictionary<AnimationParameters, int> animationParamsDict = new Dictionary<AnimationParameters, int>();

        void Awake()
        {
            AnimationParameters[] animStateParams = Enum.GetValues(typeof(AnimationParameters)) as AnimationParameters[];

            foreach (var e in animStateParams)
            {
                // Debug.Log($"{e.ToString()}");
                animationParamsDict.Add(e, Animator.StringToHash(e.ToString()));
            }
        }
    }
}
