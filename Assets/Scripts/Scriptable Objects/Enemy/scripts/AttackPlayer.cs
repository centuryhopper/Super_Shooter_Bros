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
            player = HealthDamageManager.instance.player;


        }

        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            EnemyMovement e = c.GetEnemyMovement(a);
            if (player is null || e is null) return;
            Vector3 enemyToPlayer = player.transform.position - e.transform.position;

            if (HealthDamageManager.instance.isPlayerDead)
            {
                // disable pathFindingAgent
                e.aiProgress.pathFindingAgent.gameObject.SetActive(false);

                // go back to idle
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.attack_player], false);
            }

            // TODO if player is out of range, transition back to start-walking state DONE
            // TODO there's a subtle bug of the enemy switching back to start-walking state and then only playing the animation but never physically moving towards the player
            if (Vector3.SqrMagnitude(enemyToPlayer) > 2f)
            {
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.attack_player], false);
                a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.start_walking], true);
            }





        }



        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.aiWalkParamsDict[AI_Walk_Transitions.attack_player], false);
        }
    }
}
