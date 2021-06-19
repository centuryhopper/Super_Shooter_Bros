using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.PathFind;
using Game.States;
using Game.Enums;
using Game.Hash;
using Game.EnemyAI;
using Game.HealthManager;
using Game.singleton;

namespace Game.EnemyAbilities
{
    // NPC follows the path finding agent. And the path finding agent follows the player

    [CreateAssetMenu(fileName = "AttackPlayer", menuName = "ability/AI/AttackPlayer", order = 0)]
    public class AttackPlayer : StateData
    {
        private Transform player;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            UnityEngine.Debug.Log($"AI is in attack mode");
            EnemyMovement e = c.GetEnemyMovement(a);
            player = GameObject.FindWithTag("Player").transform;


        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);
            Vector3 enemyToPlayer = player.transform.position - e.transform.position;

            // TODO if player is out of range, transition back to start-walking state
            if (Vector3.SqrMagnitude(enemyToPlayer) > 2f)
            {
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], true);
            }

            

        }



        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.attack_player], false);
        }
    }
}
