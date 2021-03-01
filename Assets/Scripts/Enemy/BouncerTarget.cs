using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

namespace Game.EnemyAI
{
    public class BouncerTarget : MonoBehaviour
    {
        void FixedUpdate()
        {
            gameObject.transform.Rotate(new Vector3(1, 0, 0), 0.5f);
        }

        void OnTriggerEnter(Collider collision)
        {
            // if the agent in training touches this gameobject, then reward it
            var agent = collision.gameObject.GetComponent<Agent>();
            if (agent != null)
            {
                agent.AddReward(1f);
                Respawn();
            }
        }

        public void Respawn()
        {
            gameObject.transform.localPosition =
                new Vector3(
                    0,
                    2f + Random.value * 5f,
                    (1 - 2 * Random.value) * 5f);
        }
    }

}