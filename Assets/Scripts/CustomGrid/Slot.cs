using System;
using UnityEngine;
using UnityEngine.Events;
using Food;

public class Slot : MonoBehaviour {
    public int x, y;
    public bool canPlace = true;
    public bool canPickUp = true;

    public Ingredient Ingredient => _ingredient;
    [SerializeField] Ingredient _ingredient;

    public SlotGrid SlotGrid => SlotGrid;
    SlotGrid _mSlotGrid;

    static UnityEvent OnSlotPlaced = new UnityEvent();
    static UnityEvent OnSlotPickedUp = new UnityEvent();

    void Awake() {
        _mSlotGrid = transform.parent.GetComponent<SlotGrid>();
    }

    // PlaceAndMove handles registering an Ingredient with the slot and physically moving stack's location to this Slot
    // Slots only allow stacks of one card (for now?)
    public bool Place(Ingredient ingredient) {
        if (!IsEmpty() || !canPlace) { return false; }
    
        // Set slot fields
        _ingredient = ingredient;
    
        // Set ingredient fields
        ingredient.transform.SetParent(transform);
        // foreach (Vector2Int pos in _ingredient.shape) {
        //     
        // }
        
        OnSlotPlaced.Invoke();
    
        return true;
    }
    
    public virtual Transform PickUpHeld() {
        if (IsEmpty() || !canPickUp) return null;
        
        // Set ingredient fields
        _ingredient.transform.SetParent(null);
        // foreach (Vector2Int pos in _ingredient.shape) {
        //     
        // }
    
        // Set slot fields
        Ingredient ing = _ingredient;
        _ingredient = null;
        
        OnSlotPickedUp.Invoke();
        
        return ing.transform;
    }
    
    public bool IsEmpty() {
        return _ingredient == null;
    }
}