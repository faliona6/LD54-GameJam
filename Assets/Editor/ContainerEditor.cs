using UnityEngine;
using UnityEditor;
using Grid;

[CustomEditor(typeof(Container))]
public class ContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Container matrixExample = (Container)target;

        int newRowCount = EditorGUILayout.IntField("Number of Rows", matrixExample.matrix.Count);
        while (newRowCount > matrixExample.matrix.Count)
        {
            matrixExample.matrix.Add(new Row());
        }
        while (newRowCount < matrixExample.matrix.Count)
        {
            matrixExample.matrix.RemoveAt(matrixExample.matrix.Count - 1);
        }

        for (int i = 0; i < matrixExample.matrix.Count; i++)
        {
            int newColCount = EditorGUILayout.IntField($"Row {i} Columns", matrixExample.matrix[i].columns.Count);
            while (newColCount > matrixExample.matrix[i].columns.Count)
            {
                matrixExample.matrix[i].columns.Add(0);
            }
            while (newColCount < matrixExample.matrix[i].columns.Count)
            {
                matrixExample.matrix[i].columns.RemoveAt(matrixExample.matrix[i].columns.Count - 1);
            }

            for (int j = 0; j < matrixExample.matrix[i].columns.Count; j++)
            {
                matrixExample.matrix[i].columns[j] = EditorGUILayout.IntField(matrixExample.matrix[i].columns[j]);
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(matrixExample);
        }
    }
}