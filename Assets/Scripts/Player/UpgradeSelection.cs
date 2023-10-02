using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Food;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Contains randomly three upgrades that the player can select at the end of the day:
// 1. Pan
// 2. SO_Augments
// 3. SO_Augments
// When the player clicks the item, an event will fire to tell listeners have clicked it

public class UpgradeSelection : MonoBehaviour {
    // These upgrades should randomize at the end of every day
    SO_Augments _augment1;
    SO_Augments _augment2;
    Pan _pan;

    public List<SO_Augments> _augmentList;

    // Rest of the items should disappear after an item is selected
    public UnityEvent OnUpgradeSelected = new UnityEvent();

    void Awake() {
        _augmentList = new List<SO_Augments>();
    }

    // call from gamemanager
    public void RandomizeUpgrades() {
        List<SO_Augments> curAugmentChoices = new List<SO_Augments>(_augmentList);
        _augment1 = curAugmentChoices[Random.Range(0, _augmentList.Count)];
        curAugmentChoices.Remove(_augment1);    // remove so we dont get same choice
        _augment2 = curAugmentChoices[Random.Range(0, _augmentList.Count)];
    }

    // public void ChooseOption(int n) {
    //     switch (n) {
    //         case 0:
    //             Factory.Instance.CreateIngredientObj()
    //             break;
    //     }
    // }
}