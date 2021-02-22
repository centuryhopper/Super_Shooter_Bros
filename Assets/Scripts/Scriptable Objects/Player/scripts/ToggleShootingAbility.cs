using System.Collections;
using System.Collections.Generic;
using Game.States;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Toggle Shooting Ability", order = 0)]
    public class ToggleShootingAbility : StateData
    {
        public bool canShoot;
        public bool onStart, onEnd;
        PlayerMovement playerMovement = null;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            Debug.Log(asi.IsName("HangingIdle") ? "shooting disabled" : "shooting enabled");

            playerMovement = c.GetPlayerMoveMent(a);

            if (onStart)
            {
                toggleShootingAbility();
            }

        }
        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            if (onEnd)
            {
                toggleShootingAbility();
            }
        }

        private void toggleShootingAbility()
        {
            if (!canShoot) playerMovement.GetComponent<Shooting>().StopAllShootingCoroutines();
            playerMovement.GetComponent<Shooting>().enabled = canShoot;
            playerMovement.rifle.SetActive(canShoot);
        }
    }
}

