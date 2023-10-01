using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
    using UnityEditor;
#endif

// Contains randomly three upgrades that the player can select at the end of the day:
// 1. Pan
// 2. SO_Augments
// 3. SO_Augments
// When the player clicks the item, an event will fire to tell listeners have clicked it

public class UpgradeSelection : MonoBehaviour
{
    // These upgrades should randomize at the end of every day
    Food.SO_Augments _augment1;
    Food.SO_Augments _augment2;
    Food.Pan _pan;

    List<Food.SO_Augments> _augmentList; 

    // Rest of the items should disappear after an item is selected
    public UnityEvent OnUpgradeSelected  = new UnityEvent();

    void Awake()
    {
        _augmentList = new List<Food.SO_Augments>();

        string[] guids = AssetDatabase.FindAssets("t:SO_Augment");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Food.SO_Augments augment = (Food.SO_Augments)AssetDatabase.LoadAssetAtPath(path, typeof(Food.SO_Augments));
            _augmentList.Add(augment);
        }

        RandomizeUpgrades();
    }

    public void RandomizeUpgrades()
    {
        _augment1 = _augmentList[Random.Range(0, _augmentList.Count)];
        _augment2 = _augmentList[Random.Range(0, _augmentList.Count)];
    }
}
