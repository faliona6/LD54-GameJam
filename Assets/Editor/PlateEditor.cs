using UnityEngine;
using UnityEditor;
using Customer;

[CustomEditor(typeof(Plate))]
public class PlateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Plate plate = (Plate)target;

        // Draw default inspector properties (like prefabName)
        DrawDefaultInspector();

        // Draw the shape matrix
        EditorGUILayout.LabelField("Shape Matrix:", EditorStyles.boldLabel);

        for (int i = 0; i < 3; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < 3; j++)
            {
                plate.shape[i, j] = EditorGUILayout.IntField(plate.shape[i, j]);
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}