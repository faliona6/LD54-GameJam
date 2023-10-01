using UnityEngine;
using UnityEditor;

namespace CustomGrid
{
    [CustomEditor(typeof(Container))]
    public class ContainerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Container matrixExample = (Container)target;

            // Prefab Name
            string prefabName = EditorGUILayout.TextField("Prefab Name:", matrixExample.prefabName);
            matrixExample.prefabName = prefabName;

            // Number of Columns
            int newColCount = EditorGUILayout.IntField("Number of Columns", matrixExample.matrix.Count);
            while (newColCount > matrixExample.matrix.Count)
            {
                matrixExample.matrix.Add(new Row());
            }
            while (newColCount < matrixExample.matrix.Count)
            {
                matrixExample.matrix.RemoveAt(matrixExample.matrix.Count - 1);
            }

            // If matrix is not empty, draw rows count and grid
            if (matrixExample.matrix.Count > 0)
            {
                int newRowCount = EditorGUILayout.IntField("Number of Rows", matrixExample.matrix[0].columns.Count);

                for (int i = 0; i < matrixExample.matrix.Count; i++)
                {
                    while (newRowCount > matrixExample.matrix[i].columns.Count)
                    {
                        matrixExample.matrix[i].columns.Add(0);
                    }
                    while (newRowCount < matrixExample.matrix[i].columns.Count)
                    {
                        matrixExample.matrix[i].columns.RemoveAt(matrixExample.matrix[i].columns.Count - 1);
                    }
                }

                // Drawing the grid with Y axis from top to bottom
                EditorGUILayout.LabelField("Matrix Values:");
                for (int j = matrixExample.matrix[0].columns.Count - 1; j >= 0; j--) // Loop through rows in reverse
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int i = 0; i < matrixExample.matrix.Count; i++) // Loop through columns
                    {
                        matrixExample.matrix[i].columns[j] = EditorGUILayout.IntField(matrixExample.matrix[i].columns[j], GUILayout.Width(40));
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(matrixExample);
            }
        }
    }
}