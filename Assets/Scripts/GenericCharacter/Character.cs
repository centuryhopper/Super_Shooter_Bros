using UnityEngine;

namespace Game.GenericCharacter
{
    /// <summary>
    /// Any Character in the game should inherit this class
    /// </summary>
    public abstract class Character : MonoBehaviour
    {
        public abstract float health { get; set; }
        public abstract float damage { get; set; }
        public abstract AnimationProgress animationProgress { get; set; }
    }
}
