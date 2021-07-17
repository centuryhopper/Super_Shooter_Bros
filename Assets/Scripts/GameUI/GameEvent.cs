using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameUI
{
    public class GameEvent : MonoBehaviour
    {
        public List<GameEventListener> listeners = new List<GameEventListener>();

        void Awake()
        {
            listeners.Clear();
        }

        /// <summary>
        /// have all listeners listen for this event.
        /// The correct one will respone
        /// </summary>
        public void Raise()
        {
            foreach (var listener in listeners)
            {
                listener.onRaiseEvent();
            }
        }
    }
}
