/*
Author: Sam Burford
Date: 11/04/21
Description: A simple GroundChecker which uses a Physics.Raycast() to detect the ground. 
Usage: Add the GroundCheck component to your GameObject e.g. your player. Then select this ScriptableObject for the GroundChecker. 
*/

using UnityEngine;

namespace BSGames.Modules.GroundCheck
{
    
    [CreateAssetMenu(menuName = "Ground Checkers/3D/Raycast")]
    public class GroundChecker_Raycast3D : GroundChecker
    {
        
        public Vector3 Origin { get => transform.position + offset; }
        
        public Vector3 offset = Vector3.zero;
        public float maxDistance = 0.5f;
        [Tooltip("This value is normalised when executed. ")]
        public Vector3 direction = new Vector3(0.0f, -1.0f, 0.0f);
        public LayerMask groundLayers;
        public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;
        
        [Header("Debug")]
        public bool alwaysDrawGizmos = false;
        public Color gizmoColour = Color.white;
        public Color groundDetectedColour = Color.blue;
        
        public override void OnGroundCheckerEnabled()
        {
            
        }

        public override void OnGroundCheckerDisabled()
        {
            
        }
        
        public override bool IsGrounded()
        {
            return Physics.Raycast(Origin, direction.normalized, maxDistance, groundLayers, triggerInteraction);
        }
        
        private void RenderGizmos()
        {
            if (IsGrounded())
                Gizmos.color = groundDetectedColour;
            else
                Gizmos.color = gizmoColour;
            
            Gizmos.DrawLine(Origin, Origin + (direction.normalized * maxDistance));
        }
        
        public override void DrawGizmos()
        {
            if (alwaysDrawGizmos == false)
                return;
            
            RenderGizmos();
        }

        public override void DrawGizmosSelected()
        {
            if (alwaysDrawGizmos)
                return;
                
            RenderGizmos();
        }
        
    }

}