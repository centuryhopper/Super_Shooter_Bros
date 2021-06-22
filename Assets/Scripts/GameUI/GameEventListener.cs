using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.GameUI
{
    public class GameEventListener : MonoBehaviour
    {
        // listen for a certain event
        [SerializeField] GameEvent specifiedEvent = null;
        [SerializeField] UnityEvent response;

        void Start()
        {
            if (specifiedEvent is null) return;

            // register this event listener to the game event's list of listeners
            if (!specifiedEvent.listeners.Contains(this))
            {
                specifiedEvent.listeners.Add(this);
            }


        }


        public void onRaiseEvent()
        {
            // execute the event being listened to
            response.Invoke();
        }
    }
}
