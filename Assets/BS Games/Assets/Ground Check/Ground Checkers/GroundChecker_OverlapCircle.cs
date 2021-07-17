/*
Author: Sam Burford
Date: 11/04/21
Description: A simple GroundChecker which uses a Physics2D.OverlapCircle() to detect the ground. 
Usage: Add the GroundCheck component to your GameObject e.g. your player. Then select this ScriptableObject for the GroundChecker. 
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BSGames.Modules.GroundCheck
{
    
    [CreateAssetMenu(menuName = "Ground Checkers/2D/Overlap Circle")]
    public class GroundChecker_OverlapCircle : GroundChecker
    {
        
        public Vector2 Origin { get => (Vector2)transform.position + offset; }
        
        public Vector2 offset = Vector2.zero;
        public float radius = 0.25f;
        public LayerMask groundLayers;
        public List<string> tagsToIgnore = new List<string>();
        
        [Header("Debug")]
        public bool alwaysDrawGizmos = false;
        public Color gizmoColour = Color.white;
        public Color groundDetectedColour = Color.blue;
        
        private void RemoveDuplicateTags()
        {
            tagsToIgnore = tagsToIgnore.Distinct().ToList();
        }
        
        public override void OnGroundCheckerEnabled()
        {
            RemoveDuplicateTags();
        }
        
        public override bool IsGrounded()
        {
            var colliders = Physics2D.OverlapCircleAll(Origin, radius, groundLayers);
            
            int totalHits = colliders.Length;
            
            if (tagsToIgnore.Count > 0)
            {
                foreach (var col in colliders)
                {
                    for (int i = 0; i < tagsToIgnore.Count; i++)
                    {
                        if (string.IsNullOrEmpty(tagsToIgnore[i]))
                            continue;
                        
                        if (col.gameObject.tag.Equals(tagsToIgnore[i]))
                            totalHits--;
                    }
                }
            }
            
            return totalHits > 0;
        }
        
        private void RenderGizmos()
        {
            if (IsGrounded())
                Gizmos.color = groundDetectedColour;
            else
                Gizmos.color = gizmoColour;
            
            Gizmos.DrawWireSphere(Origin, radius);
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