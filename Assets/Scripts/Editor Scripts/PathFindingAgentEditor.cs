using System.Collections;
using System.Collections.Generic;
using Game.PathFind;
using UnityEditor;
using UnityEngine;

namespace Game.Editors
{
    [CustomEditor(typeof(PathFindingAgent))]
    public class PathFindingAgentEditor : Editor
    {
        // overridden to make a custom editor
        public override void OnInspectorGUI()
        {
            // Draws the built-in inspector.
            DrawDefaultInspector();

            // target is the object being inspected.
            var pathFindingAgent = (PathFindingAgent) target;
            if (GUILayout.Button("Go to target"))
            {
                pathFindingAgent.GoToTarget();
            }
        }
    }

}