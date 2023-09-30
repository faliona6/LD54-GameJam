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

            // Number of Rows
            int newRowCount = EditorGUILayout.IntField("Number of Rows", matrixExample.matrix.Count);
            while (newRowCount > matrixExample.matrix.Count)
            {
                matrixExample.matrix.Add(new Row());
            }
            while (newRowCount < matrixExample.matrix.Count)
            {
                matrixExample.matrix.RemoveAt(matrixExample.matrix.Count - 1);
            }

            // If matrix is not empty, draw columns count and grid
            if (matrixExample.matrix.Count > 0)
            {
                int newColCount = EditorGUILayout.IntField("Number of Columns", matrixExample.matrix[0].columns.Count);

                for (int i = 0; i < matrixExample.matrix.Count; i++)
                {
                    while (newColCount > matrixExample.matrix[i].columns.Count)
                    {
                        matrixExample.matrix[i].columns.Add(0);
                    }
                    while (newColCount < matrixExample.matrix[i].columns.Count)
                    {
                        matrixExample.matrix[i].columns.RemoveAt(matrixExample.matrix[i].columns.Count - 1);
                    }
                }

                // Drawing the grid
                EditorGUILayout.LabelField("Matrix Values:");
                for (int i = 0; i < matrixExample.matrix.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    for (int j = 0; j < matrixExample.matrix[i].columns.Count; j++)
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