using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;

public class Pantry : MonoBehaviour
{
    public Container container;
    
    public SlotGrid slotGrid;

    void Start() {
        slotGrid.Init(container);
    }
    
    
}
