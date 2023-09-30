using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;

[RequireComponent(typeof(SlotGrid))]
public class PlayerInventory : MonoBehaviour {
    public Container container;
    
    public SlotGrid slotGrid;

    void Start() {
        slotGrid.Init(container);
    }
}