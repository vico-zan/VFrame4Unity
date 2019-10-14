using UnityEngine;
using UnityEditor;
using VFrame;

namespace VEditor {
    [CanEditMultipleObjects]
    [CustomEditor (typeof (RFPage), true)]
    public class RFPageEditor : Editor {
        NScene m_mask;

        public override void OnInspectorGUI () {
            base.OnInspectorGUI ();
            serializedObject.Update ();

            SerializedProperty et = serializedObject.FindProperty ("m_sceneMask");
            //		Debug.Log (et.intValue);
            if (et.intValue == -1) {
                m_mask = 0;
                foreach (NScene scene in NScene.GetValues (typeof (NScene))) {
                    m_mask |= scene;
                }
            }
            else
                m_mask = (NScene)et.intValue;

            m_mask = (NScene)EditorGUILayout.EnumFlagsField (new GUIContent ("Scene Mask"), m_mask);
            //		Debug.Log (m_mask);
            et.intValue = (int)m_mask;

            EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_showType"), new GUIContent ("Show Type"));
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_enableStack"), new GUIContent ("Enable Stack"));
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_showPrior"), new GUIContent ("Show Prior"));
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_showLayer"), new GUIContent ("Show Layer"));
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_tEnable"), new GUIContent ("Enable Transition Type"));
            EditorGUILayout.PropertyField (serializedObject.FindProperty ("m_tDisable"), new GUIContent ("Disable Transition Type"));

            serializedObject.ApplyModifiedProperties ();
        }
    }
}