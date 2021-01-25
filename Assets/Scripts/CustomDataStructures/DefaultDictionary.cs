using System.Collections.Generic;
using UnityEngine;

namespace Game.data_structures
{
    /// <summary>
    /// Just like a dictionary
    /// but will never throw a key error exception.
    /// "TValue : new()" is the key concept to understand here
    /// because TValue will be set to the default value according to
    /// .Net if the TKey was not found (i.e. int => 0, bool => false, etc.)
    /// Known limitation: TValue must have a parameterless constructor (e.g. string will not work)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new()
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue val;
                if (!TryGetValue(key, out val))
                {
                    val = new TValue();
                    Add(key, val);
                }
                return val;
            }
            set { base[key] = value; }
        }
    }
}



