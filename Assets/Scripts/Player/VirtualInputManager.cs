// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

namespace Game.PlayerCharacter
{
    /// <summary>
    /// This script will be used to control the player and
    /// can be controlled by any input script, so that
    /// it is cross-platform friendly.
    /// Note that this script will get instantiated in the scene at the start of the game if
    /// a monobehaviour script tries to access it
    /// </summary>
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool jump { get; set; }
        public bool moveLeft { get; set; }
        public bool moveRight { get; set; }
    }
}
