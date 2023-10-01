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
            for (int y = 0; y < container.matrix[0].columns.Count; y++) {
                if (container.matrix[x].columns[y] == 1) {
                    Slot slot = Instantiate(_slotPrefab, transform).GetComponent<Slot>();
                    slot.transform.localPosition = new Vector3(x, y, 0);
                    slot.x = x;
                    slot.y = y;

                    slotGrid[new Vector2Int(x, y)] = slot;
                }
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

    public bool IsInBounds(int x, int y) { return slotGrid.ContainsKey(new Vector2Int(x, y)); }
    // public bool IsInBounds(int x, int y) { return !(x < 0 || x >= _width || y < 0 || y >= _height); }
}