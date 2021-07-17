/*
Author: Sam Burford
Date: 11/04/21
Description: A simple GroundChecker which uses a Physics2D.Raycast() to detect the ground. 
Usage: Add the GroundCheck component to your GameObject e.g. your player. Then select this ScriptableObject for the GroundChecker. 
*/

using UnityEngine;

namespace BSGames.Modules.GroundCheck
{
    
    [CreateAssetMenu(menuName = "Ground Checkers/2D/Raycast")]
    public class GroundChecker_Raycast2D : GroundChecker
    {
        
        public Vector2 Origin { get => (Vector2)transform.position + offset; }
        
        public Vector2 offset = Vector2.zero;
        public float distance = 0.5f;
        [Tooltip("This value is normalised when executed. ")]
        public Vector2 direction = new Vector2(0.0f, -1.0f);
        public LayerMask groundLayers;
        
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
            return Physics2D.Raycast(Origin, direction.normalized, distance, groundLayers);
        }
        
        private void RenderGizmos()
        {
            if (IsGrounded())
                Gizmos.color = groundDetectedColour;
            else
                Gizmos.color = gizmoColour;
            
            Gizmos.DrawLine(Origin, Origin + (direction.normalized * distance));
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