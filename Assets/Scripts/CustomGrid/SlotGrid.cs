using System.Collections.Generic;
using CustomGrid;
using Food;
using UnityEngine;

public class SlotGrid : MonoBehaviour {
    public Dictionary<Vector2Int, Slot> slotGrid = new();

    // public int Width => _width;
    // public int Height => _height;
    // [SerializeField] int _width, _height;

    [SerializeField] GameObject _slotPrefab;

    public void Init(Container container) {
        for (int x = 0; x < container.matrix.Count; x++) {
            for (int y = 0; y < container.matrix[0].columns.Count; y++)
            {
                int value = container.matrix[x].columns[y];
                if (value is not (1 or 2))
                {
                    continue;
                }
                
                Slot.SlotType type = value == 1 ? Slot.SlotType.Empty : Slot.SlotType.Cooking;
                    
                Slot slot = Instantiate(_slotPrefab, transform).GetComponent<Slot>();
                slot.SetSlotType(type);
                slot.transform.localPosition = new Vector3(x, y, 0);
                slot.x = x;
                slot.y = y;

                slotGrid[new Vector2Int(x, y)] = slot;
            }
        }
    }

    public Slot SelectSlotRelative(Vector2Int origin, Vector2Int relativePos) {
        int targetX = origin.x + relativePos.x;
        int targetY = origin.y + relativePos.y;

        if (!IsInBounds(targetX, targetY)) {
            return null;
        }

        return slotGrid[new Vector2Int(targetX, targetY)];
    }

    // Returns slot using absolute position.
    public Slot SelectSlot(Vector2Int pos) {
        if (!IsInBounds(pos.x, pos.y)) {
            return null;
        }

        return slotGrid[pos];
    }

    public List<Ingredient> GetIngredients() {
        List<Ingredient> ingredients = new List<Ingredient>();
        foreach (KeyValuePair<Vector2Int, Slot> slot in slotGrid)
        {
            // Check if the slot's ingredient is not already in the list before adding.
            if (slot.Value.Ingredient != null && !ingredients.Contains(slot.Value.Ingredient))
            {
                ingredients.Add(slot.Value.Ingredient);
            }
        }

        return ingredients;
    }

    public List<Slot> GetSlotsOfType(Slot.SlotType type)
    {
        List<Slot> slots = new List<Slot>();
        foreach (KeyValuePair<Vector2Int, Slot> slot in slotGrid)
        {
            if (slot.Value.GetSlotType().Equals(type))
            {
                slots.Add(slot.Value);
            }
        }
        return slots;
    }

    public bool IsInBounds(int x, int y) { return slotGrid.ContainsKey(new Vector2Int(x, y)); }
    // public bool IsInBounds(int x, int y) { return !(x < 0 || x >= _width || y < 0 || y >= _height); }
}