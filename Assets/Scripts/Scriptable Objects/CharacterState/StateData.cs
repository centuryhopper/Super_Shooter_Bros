using UnityEngine;

namespace Game.States
{
    /// <summary>
    /// The base model for every player action/ability that has
    /// and will be created.
    /// Always get the player movement script from the CharacterStateBase class
    /// </summary>
    public abstract class StateData : ScriptableObject
    {
        public abstract void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi);
        public abstract void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi);
        public abstract void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi);
    }
}
