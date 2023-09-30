using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Customer
{
    [CreateAssetMenu(fileName = "NewPlate", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class Plate : ScriptableObject
    {
        public string prefabName;
        public int[,] shape = new int[3, 3];
    }
}
