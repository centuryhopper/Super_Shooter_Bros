using System;
using System.Collections.Generic;
using UnityEngine;
using Game.singleton;
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

        public Dictionary<int, AnimationStateNames> stateNamesDict = new Dictionary<int, AnimationStateNames>();

        public Dictionary<AI_Walk_Transitions, int> aiWalkParamsDict = new Dictionary<AI_Walk_Transitions, int>();

        void Awake()
        {
            BuildStateNameDict();
            ConvertEnumsStringsToInt<AnimationParameters>(animationParamsDict);
            ConvertEnumsStringsToInt<AI_Walk_Transitions>(aiWalkParamsDict);
        }

        private void ConvertEnumsStringsToInt<T>(Dictionary<T,int> d) where T : Enum
        {
            T[] aiWalkParams = Enum.GetValues(typeof(T)) as T[];

            // convert each stringified enum to its hashed integer
            foreach (var e in aiWalkParams)
            {
                d[e] = Animator.StringToHash(e.ToString());
            }
        }


        private void BuildStateNameDict()
        {
            AnimationStateNames[] animStateNames = Enum.GetValues(typeof(AnimationStateNames)) as AnimationStateNames[];

            if (animStateNames == null)
            {
                Debug.LogWarning($"Couldn't find state names");
                return;
            }

            foreach (var stateName in animStateNames)
            {
                stateNamesDict[Animator.StringToHash(stateName.ToString())] = stateName;
            }
        }
        // private void BuildStateParamDict()
        // {
        //     AnimationParameters[] animStateParams = Enum.GetValues(typeof(AnimationParameters)) as AnimationParameters[];

        //     foreach (var e in animStateParams)
        //     {
        //         // Debug.Log($"{e.ToString()}");
        //         animationParamsDict[e] = Animator.StringToHash(e.ToString());
        //     }
        // }
    }
}
