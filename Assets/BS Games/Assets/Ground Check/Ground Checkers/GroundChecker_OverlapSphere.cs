/*
Author: Sam Burford
Date: 11/04/21
Description: A simple GroundChecker which uses a Physics.SpherecastAll() to detect the ground. 
Usage: Add the GroundCheck component to your GameObject e.g. your player. Then select this ScriptableObject for the GroundChecker. 
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BSGames.Modules.GroundCheck
{
    
    [CreateAssetMenu(menuName = "Ground Checkers/3D/Overlap Sphere")]
    public class GroundChecker_OverlapSphere : GroundChecker
    {
        
        public Vector3 Origin { get => transform.position + offset; }
        
        public Vector3 offset = Vector3.zero;
        public float radius = 0.25f;
        public LayerMask groundLayers;
        public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;
        public List<string> tagsToIgnore = new List<string>();
        
        [Header("Debug")]
        public bool alwaysDrawGizmos = false;
        public Color gizmoColour = Color.white;
        public Color groundDetectedColour = Color.blue;
        
        public override void OnGroundCheckerEnabled()
        {
            RemoveDuplicateTags();
        }

        public override void OnGroundCheckerDisabled()
        {
            
        }
        
        private void RemoveDuplicateTags()
        {
            tagsToIgnore = tagsToIgnore.Distinct().ToList();
        }
        
        public override bool IsGrounded()
        {
            var hits = Physics.OverlapSphere(Origin, radius, groundLayers, triggerInteraction);
            
            int totalHits = hits.Length;
            
            if (tagsToIgnore.Count > 0)
            {
                foreach (var hit in hits)
                {
                    for (int i = 0; i < tagsToIgnore.Count; i++)
                    {
                        if (string.IsNullOrEmpty(tagsToIgnore[i]))
                            continue;
                        
                        if (hit.gameObject.tag.Equals(tagsToIgnore[i]))
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