using UnityEngine;

namespace Game.PlayerCharacter
{
    /// <summary>
    /// the base model for every player action/ability that has
    /// and will be created.
    /// Always get the player movement script from the CharacterStateBase class
    /// </summary>
    public abstract class StateData : ScriptableObject
    {
        public abstract void OnEnter(PlayerState c, Animator a, AnimatorStateInfo asi);
        public abstract void OnAbilityUpdate(PlayerState c, Animator a, AnimatorStateInfo asi);
        public abstract void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi);
    }
}
