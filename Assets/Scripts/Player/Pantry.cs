using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;

[RequireComponent(typeof(SlotGrid))]
public class Pantry : MonoBehaviour
{
    public Container container;
    
    SlotGrid _slotGrid;

    void Start() {
        _slotGrid = GetComponent<SlotGrid>();
        
        _slotGrid.Init(container);
        foreach (KeyValuePair<Vector2Int, Slot> pos in _slotGrid.slotGrid) {
            pos.Value.canPlace = false;
        }
    }
}
