using System;
using UnityEngine;
using UnityEngine.Events;
using Food;
using UnityEngine.UIElements;

public class Slot : MonoBehaviour {
    public int x, y;
    public bool canPlace = true;
    public bool canPickUp = true;
    public Ingredient Ingredient => _ingredient;
    [SerializeField] Ingredient _ingredient;

    public SlotGrid SlotGrid => _mSlotGrid;
    SlotGrid _mSlotGrid;

    public UnityEvent OnSlotPlaced = new UnityEvent();
    public UnityEvent OnSlotPickedUp = new UnityEvent();

    private SlotType slotType = SlotType.Empty;

    void Awake() { _mSlotGrid = transform.parent.GetComponent<SlotGrid>(); }

    public enum SlotType
    {
        Empty,
        Cooking,
        Plate,
    }

    // Place handles registering an Ingredient with the slot
    public bool Place(Ingredient ingredient) {
        // Validation
        if (slotType == SlotType.Plate && (!ingredient.cooked || ingredient.burned)) {
            return false;
        }
        foreach (Vector2Int offset in ingredient.shape) { // should contain center
            Slot s = _mSlotGrid.SelectSlotRelative(new Vector2Int(x, y), offset);
            if (!s || !s.IsEmpty() || !s.canPlace || !slotType.Equals(s.GetSlotType()) ) {
                return false;
            }
        }

        // Set slot fields
        foreach (Vector2Int offset in ingredient.shape) {
            Slot s = _mSlotGrid.SelectSlotRelative(new Vector2Int(x, y), offset);
            s._ingredient = ingredient;
        }

        // Set ingredient fields
        ingredient.transform.SetParent(transform);

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
        foreach (Vector2Int offset in ing.shape) {
            Slot s = _mSlotGrid.SelectSlotRelative(new Vector2Int(x, y), offset);
            s._ingredient = null;
        }

        OnSlotPickedUp.Invoke();

        return ing.transform;
    }

    public void SetSlotType(SlotType type)
    {
        slotType = type;
    }

    public SlotType GetSlotType() => slotType;

    public bool IsEmpty() { return _ingredient == null; }
}