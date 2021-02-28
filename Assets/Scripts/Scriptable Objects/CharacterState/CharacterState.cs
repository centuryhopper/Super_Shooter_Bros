using System.Collections.Generic;
using Game.PlayerCharacter;
using UnityEngine;

namespace Game.States
{
    public class CharacterState : StateMachineBehaviour
    {
        private PlayerMovement playerMovement;
        public PlayerMovement GetPlayerMoveMent(Animator animator)
        {
            if (playerMovement == null)
            {
                // was getcomponentinparent
                playerMovement = animator.GetComponentInParent<PlayerMovement>();
            }

            return playerMovement;
        }

        // list of scriptable objects
        public List<StateData> abilityDataLst = new List<StateData>();

        public void UpdateAll(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            for (int i = 0; i < abilityDataLst.Count; ++i)
            {
                if (abilityDataLst[i] == null) {Debug.LogWarning("abilityDataLst[i] OnStateUpdate is null"); return;}
                abilityDataLst[i].OnAbilityUpdate(c, a, asi);
            }
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            for (int i = 0; i < abilityDataLst.Count; ++i)
            {
                if (abilityDataLst[i] == null) {Debug.LogWarning("abilityDataLst[i] in OnStateEnter is null"); return;}
                abilityDataLst[i].OnEnter(this, animator, animatorStateInfo);
            }
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            UpdateAll(this, animator, animatorStateInfo);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            for (int i = 0; i < abilityDataLst.Count; ++i)
            {
                if (abilityDataLst[i] == null) {Debug.LogWarning("abilityDataLst[i] in OnStateExit is null"); return;}
                abilityDataLst[i].OnExit(this, animator, animatorStateInfo);
            }

        }
    }
}
