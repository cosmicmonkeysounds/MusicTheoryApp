namespace Dreamteck.Splines.Editor
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;

    [CustomEditor(typeof(SplinePositioner), true)]
    [CanEditMultipleObjects]
    public class SplinePositionerEditor : SplineTracerEditor
    {
        protected override void BodyGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Positioning", EditorStyles.boldLabel);

            serializedObject.Update();
            SerializedProperty mode = serializedObject.FindProperty("_mode");
            SerializedProperty position = serializedObject.FindProperty("_position");
            EditorGUI.BeginChangeCheck();
            SplinePositioner positioner = (SplinePositioner)target;
            EditorGUILayout.PropertyField(mode, new GUIContent("Mode"));
            if (positioner.mode == SplinePositioner.Mode.Distance)
            {
                float lastPos = position.floatValue;
                EditorGUILayout.PropertyField(position, new GUIContent("Distance"));
                if (lastPos != position.floatValue)
                {
                    positioner.position = position.floatValue;
                }
            }
            else
            {
                SerializedProperty percent = serializedObject.FindProperty("_result").FindPropertyRelative("percent");

                EditorGUILayout.BeginHorizontal();
                
                double pos = positioner.ClipPercent(percent.floatValue);
                EditorGUI.BeginChangeCheck();
                pos = EditorGUILayout.Slider("Percent", (float)pos, 0f, 1f);
                if (EditorGUI.EndChangeCheck())
                {
                    position.floatValue = (float)pos;
                    serializedObject.ApplyModifiedProperties();
                }
                if (GUILayout.Button("Set Distance", GUILayout.Width(85)))
                {
                    DistanceWindow w = EditorWindow.GetWindow<DistanceWindow>(true);
                    w.Init(OnSetDistance, positioner.CalculateLength());
                }
                EditorGUILayout.EndHorizontal();
            }
            SerializedProperty targetObject = serializedObject.FindProperty("_targetObject");
            EditorGUILayout.PropertyField(targetObject, new GUIContent("Target Object"));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
            base.BodyGUI();
        }

        void OnSetDistance(float distance)
        {
            int longest = 0;
            float max = 0f;
            for (int i = 0; i < users.Length; i++)
            {
                float length = users[i].CalculateLength();
                if (length > max)
                {
                    max = length;
                    longest = i;
                }
            }
            SerializedProperty position = serializedObject.FindProperty("_position");
            SplinePositioner positioner = (SplinePositioner)targets[longest];
            double travel = positioner.Travel(0.0, distance, Spline.Direction.Forward);
            position.floatValue = (float)travel;
            serializedObject.ApplyModifiedProperties();

            for (int i = 0; i < targets.Length; i++)
            {
                positioner = (SplinePositioner)targets[0];
                positioner.position = travel;
            }
        }
    }
}
