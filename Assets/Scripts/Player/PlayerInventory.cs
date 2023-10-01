using CustomGrid;
using UnityEngine;

[RequireComponent(typeof(SlotGrid))]
public class PlayerInventory : MonoBehaviour {
    public Container container;
    
    public SlotGrid slotGrid;

    void Start() {
        slotGrid.Init(container);
    }
}