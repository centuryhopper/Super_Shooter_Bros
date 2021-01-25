using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enums
{
    public enum AnimationStateNames
    {
        // base layer
        Idle,

        // air movement
        Jump_Prep,
        Jump_Normal,
        Jump_Landing,
        FrontFlip,
        Falling,

        // ledge grab
        HangingIdle,
        LedgeClimb,

        // land movement
        Walk,
        Run,
        RunToStop,
        ForwardRoll,
    }
}
