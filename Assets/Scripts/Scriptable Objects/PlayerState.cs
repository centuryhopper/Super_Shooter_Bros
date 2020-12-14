using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerCharacter
{
    public class PlayerState : StateMachineBehaviour
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

        private PlayerController playerController;
        public PlayerController GetPlayerController(Animator animator)
        {
            if (playerController == null)
            {
                // was getcomponentinparent
                playerController = animator.GetComponentInParent<PlayerController>();
            }

            return playerController;
        }

        // list of scriptable objects
        public List<StateData> abilityDataLst = new List<StateData>();

        public void UpdateAll(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            foreach (StateData d in abilityDataLst)
            {
                if (d == null) Debug.Log("d is null");
                d.OnAbilityUpdate(c, a, asi);
            }
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            foreach (StateData d in abilityDataLst)
            {
                d.OnEnter(this, animator, animatorStateInfo);
            }
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            UpdateAll(this, animator, animatorStateInfo);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            foreach (StateData d in abilityDataLst)
            {
                d.OnExit(this, animator, animatorStateInfo);
            }
        }
    }
}
