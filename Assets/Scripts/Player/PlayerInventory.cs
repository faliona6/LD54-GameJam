using CustomGrid;
using UnityEngine;

[RequireComponent(typeof(SlotGrid))]
public class PlayerInventory : MonoBehaviour {
    public Container container;
    
    public SlotGrid slotGrid;

    void Awake() {
        slotGrid.Init(container);
    }

    void Init() {
        // TODO: fill with initial subset of InfiniteIngredients
    }
}