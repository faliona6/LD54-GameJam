using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomGrid
{
    [System.Serializable]
    public class Row
    {
        public List<int> columns = new List<int>();
    }

    [CreateAssetMenu(fileName = "NewContainer", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class Container : ScriptableObject
    {
        public string prefabName;
        [SerializeField]
        public List<Row> matrix = new List<Row>();
        public Vector2Int center;
    }
}
