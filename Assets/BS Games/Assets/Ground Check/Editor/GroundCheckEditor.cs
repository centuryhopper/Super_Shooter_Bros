using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BSGames.Modules.GroundCheck.Editor
{
    
    [CustomEditor(typeof(GroundCheck))]
    public class GroundCheckEditor : UnityEditor.Editor
    {
        
        private SerializedProperty scriptableObject = null;
        private bool showSettings = false;
        
        private void OnEnable()
        {
            scriptableObject = serializedObject.FindProperty("m_groundChecker");
        }
        
        private void DrawSettings()
        {
            string message = "Note: these settings are shared with the ScriptableObject instance. All GroundCheck components with this instance will have the same values. If you want different values you must create a new instance. ";
            EditorGUILayout.HelpBox(message, MessageType.Info, true);
            
            EditorGUILayout.Space();
            
            SerializedObject obj = new UnityEditor.SerializedObject(scriptableObject.objectReferenceValue);
            SerializedProperty propertyIterator = obj.GetIterator();
            bool enterChildren = true;
            
            obj.Update();
                
            while (propertyIterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                
                if (propertyIterator.name == "m_Script")
                    continue;
                
                EditorGUILayout.PropertyField(propertyIterator, true);
            }
            
            obj.ApplyModifiedProperties();
            
            if (scriptableObject.hasChildren == false)
                GUILayout.Label(new GUIContent("No settings."));
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            GUIStyle centreLabelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            GUILayout.Label(new GUIContent("Scriptable Object Reference"), centreLabelStyle);
            EditorGUILayout.PropertyField(scriptableObject);
            
            serializedObject.ApplyModifiedProperties();
            
            if (scriptableObject.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("You must assign a scriptable object!", MessageType.Warning);
                return;
            }
            
            EditorGUILayout.Space();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Settings"), centreLabelStyle);
            
            if (GUILayout.Button("Show/Hide", EditorStyles.miniButton, GUILayout.ExpandWidth(false)))
                showSettings = !showSettings;
            
            GUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            
            if (showSettings)
            {
                DrawSettings();
            }
            
            serializedObject.ApplyModifiedProperties();
        }
        
    }
    
}