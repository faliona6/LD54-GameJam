using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorPanel : MonoBehaviour {
    [SerializeField] Sprite flavorIconSprite;
    [SerializeField] GameObject flavorIcon1;
    [SerializeField] GameObject flavorIcon2;
    [SerializeField] GameObject flavorIcon3;
    [SerializeField] GameObject flavorIcon4;
    [SerializeField] GameObject flavorIcon5;

    // lol
    void Awake() {
        flavorIcon1.SetActive(false);
        flavorIcon2.SetActive(false);
        flavorIcon3.SetActive(false);
        flavorIcon4.SetActive(false);
        flavorIcon5.SetActive(false);
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs) {
            sr.sprite = flavorIconSprite;
        }
    }

    // lolol
    public void DisplayFlavorIcon(int numFlavor) {
        switch (numFlavor) {
            case 0:
                break;
            case 1:
                flavorIcon1.SetActive(true);
                break;
            case 2:
                flavorIcon1.SetActive(true);
                flavorIcon2.SetActive(true);
                break;
            case 3:
                flavorIcon1.SetActive(true);
                flavorIcon2.SetActive(true);
                flavorIcon3.SetActive(true);
                break;
            case 4:
                flavorIcon1.SetActive(true);
                flavorIcon2.SetActive(true);
                flavorIcon3.SetActive(true);
                flavorIcon4.SetActive(true);
                break;
            case 5:
                flavorIcon1.SetActive(true);
                flavorIcon2.SetActive(true);
                flavorIcon3.SetActive(true);
                flavorIcon4.SetActive(true);
                flavorIcon5.SetActive(true);
                break;
            default:
                Debug.LogError("you done messed up you put wrong number of flavor: " + numFlavor);
                break;
        }
    }
}
