using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

namespace Game.EnemyAI
{
    public class BouncerAgent : Agent
    {
        public GameObject target;
        public GameObject agentObject;
        public float strength = 350f;
        Rigidbody agentRigidbody;
        Vector3 orientation;
        float jumpCoolDown;
        int totalJumps = 20;
        int jumpsLeft = 20;
        EnvironmentParameters defaultParams;

        public override void Initialize()
        {
            base.Initialize();
            agentRigidbody = GetComponent<Rigidbody>();
            orientation = Vector3.zero;
            defaultParams = Academy.Instance.EnvironmentParameters;
        }

        //this doesn't need to be changed for the 2D conversion
        public override void CollectObservations(VectorSensor sensor)
        {
            base.CollectObservations(sensor);
            sensor.AddObservation(target.transform.position);
            sensor.AddObservation(agentObject.transform.position);
        }

        public override void OnActionReceived(float[] vectorAction)
        {
            base.OnActionReceived(vectorAction);

            for (var i = 0; i < vectorAction.Length; i++)
            {
                //this clamps vectorAction[i] to a value between -1 and 1. Guess that means if it's above 1 or below -1, it flatlines
                vectorAction[i] = Mathf.Clamp(vectorAction[i], -1f, 1f);
            }
            float x = vectorAction[0];
            float y = ScaleAction(vectorAction[1], 0, 1);
            float z = vectorAction[2];

            //adds force to rigid body for what purpose? Does it move it around? This might be the place I need to change
            //basic physics
            agentRigidbody.AddForce(new Vector3(0, y + 1, z) * strength);

            AddReward(-0.05f * (vectorAction[0] * vectorAction[0] + vectorAction[1] * vectorAction[1]
                + vectorAction[2] * vectorAction[2]) / 3f);

            orientation = new Vector3(0, y, z);
        }

        public override void Heuristic(float[] actionsOut)
        {
            //what is base.heuristic
            base.Heuristic(actionsOut);

            actionsOut[0] = Input.GetAxis("Horizontal");
            actionsOut[1] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
            actionsOut[2] = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), 0.51f) && jumpCoolDown <= 0f)
            {

                // Forces a decision, zeros out velocity, and decrements 'jumpsLeft'

                RequestDecision();
                jumpsLeft -= 1;
                jumpCoolDown = 0.1f;
                agentRigidbody.velocity = default(Vector3);
            }

            jumpCoolDown -= Time.fixedDeltaTime;

            if (gameObject.transform.position.y < -1)
            {

                //When the agent falls off the plane

                AddReward(-1);
                EndEpisode();
                return;
            }

            //this can be left alone. I'll only be ignoring the agents movement on the x. Checking for its position along the x is redundant but ok
            if (gameObject.transform.localPosition.x < -17 || gameObject.transform.localPosition.x > 17
                || gameObject.transform.localPosition.z < -17 || gameObject.transform.localPosition.z > 17)
            {

                //When the agent goes beyond the plane

                AddReward(-1);
                EndEpisode();
                return;
            }
            if (jumpsLeft == 0)
            {
                EndEpisode();
            }
        }

        private void Update()
        {
            //rotation? Uh oh this will be troublesome for my 2D conversion. I'll have to make sure it only rotates on one axis
            if (orientation.magnitude > float.Epsilon)
            {

                //Quaternion lookrotation defines where the agent is facing relative to a defined "up" parameter
                agentObject.transform.rotation = Quaternion.Lerp(agentObject.transform.rotation,
                    Quaternion.LookRotation(orientation),
                    Time.deltaTime * 10f);
            }
        }

        //the spawn location
        public override void OnEpisodeBegin()
        {
            base.OnEpisodeBegin();

            //here it is calculating the new position coordinates... but it seems to be when the episode begins.
            // this needs to be changed to only spawn along the y and z, so I set x = 0
            //gameObject.transform.localPosition = new Vector3(
            //    (1 - 2 * Random.value) * 5, 2, (1 - 2 * Random.value) * 5);

            //change all x to 0 so it only spawns on the 2D plane
            gameObject.transform.localPosition = new Vector3(
                0, 2, (1 - 2 * Random.value) * 5);

            agentRigidbody.velocity = Vector3.zero;

            var environment = gameObject.transform.parent.gameObject;
            var targets =
                environment.GetComponentsInChildren<BouncerTarget>();
            foreach (var t in targets)
            {
                t.Respawn();
            }
            //resets total jumps
            jumpsLeft = totalJumps;

            ResetParamters();
        }

        //NOT WHERE THE JUMP DIRECTION IS CALCULATED
        public void ResetParamters()
        {
            var targetScale = defaultParams.GetWithDefault("target_scale", 1.0f);
            target.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
        }
    }
}
























// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Unity.MLAgents;
// using Unity.MLAgents.Sensors;

// public class BouncerAgent : Agent
// {
//     public GameObject target;
//     public GameObject agentObject;
//     public float strength = 350f;

//     Rigidbody agentRigidbody;
//     Vector3 orientation;
//     float jumpCoolDown;
//     int totalJumps = 20;
//     int jumpsLeft = 20;
//     EnvironmentParameters defaultParams;

//     public override void Initialize()
//     {
//         base.Initialize();
//         agentRigidbody = GetComponent<Rigidbody>();
//         orientation = Vector3.zero;
//         defaultParams = Academy.Instance.EnvironmentParameters;
//     }

//     public override void CollectObservations(VectorSensor sensor)
//     {
//         base.CollectObservations(sensor);
//         sensor.AddObservation(target.transform.position);
//         sensor.AddObservation(agentObject.transform.position);
//     }

//     public override void OnActionReceived(float[] vectorAction)
//     {
//         base.OnActionReceived(vectorAction);

//         for (var i = 0; i < vectorAction.Length; i++)
//         {
//             vectorAction[i] = Mathf.Clamp(vectorAction[i], -1f, 1f);
//         }
//         float x = vectorAction[0];
//         float y = ScaleAction(vectorAction[1], 0, 1);
//         float z = vectorAction[2];
//         agentRigidbody.AddForce(new Vector3(x, y + 1, z) * strength);

//         AddReward(-0.05f * (
//             vectorAction[0] * vectorAction[0] +
//             vectorAction[1] * vectorAction[1] +
//             vectorAction[2] * vectorAction[2]) / 3f);

//         orientation = new Vector3(x, y, z);
//     }

//     public override void Heuristic(float[] actionsOut)
//     {
//         base.Heuristic(actionsOut);

//         actionsOut[0] = Input.GetAxis("Horizontal");
//         actionsOut[1] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
//         actionsOut[2] = Input.GetAxis("Vertical");
//     }

//     private void FixedUpdate()
//     {
//         if (Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), 0.51f) && jumpCoolDown <= 0f)
//         {

//             //Forces a decision, zeros out velocity, and decrements 'jumpsLeft'

//             RequestDecision();
//             jumpsLeft -= 1;
//             jumpCoolDown = 0.1f;
//             agentRigidbody.velocity = default(Vector3);
//         }

//         jumpCoolDown -= Time.fixedDeltaTime;

//         if (gameObject.transform.position.y < -1)
//         {

//             //When the agent falls off the plane

//             AddReward(-1);
//             EndEpisode();
//             return;
//         }

//         if (gameObject.transform.localPosition.x < -17 || gameObject.transform.localPosition.x > 17
//             || gameObject.transform.localPosition.z < -17 || gameObject.transform.localPosition.z > 17)
//         {

//             //When the agent goes beyond the plane

//             AddReward(-1);
//             EndEpisode();
//             return;
//         }
//         if (jumpsLeft == 0)
//         {
//             EndEpisode();
//         }
//     }

//     private void Update()
//     {
//         if (orientation.magnitude > float.Epsilon)
//         {
//             agentObject.transform.rotation = Quaternion.Lerp(agentObject.transform.rotation,
//                 Quaternion.LookRotation(orientation),
//                 Time.deltaTime * 10f);
//         }
//     }

//     public override void OnEpisodeBegin()
//     {
//         base.OnEpisodeBegin();

//         gameObject.transform.localPosition = new Vector3(
//             (1 - 2 * Random.value) * 5, 2, (1 - 2 * Random.value) * 5);
//         agentRigidbody.velocity = Vector3.zero;

//         var environment = gameObject.transform.parent.gameObject;
//         var targets =
//             environment.GetComponentsInChildren<BouncerTarget>();
//         foreach (var t in targets)
//         {
//             t.Respawn();
//         }
//         jumpsLeft = totalJumps;

//         ResetParamters();
//     }

//     public void ResetParamters()
//     {
//         var targetScale = defaultParams.GetWithDefault("target_scale", 1.0f);
//         target.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
//     }
// }