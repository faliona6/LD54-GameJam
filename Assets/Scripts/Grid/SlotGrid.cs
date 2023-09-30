using System;
using UnityEngine;

public class SlotGrid : MonoBehaviour {
    public Slot[,] slotGrid;

    public int Width => width;
    public int Height => height;
    [SerializeField] int width, height;

    void Awake() {
        SetupSlotGrid();
    }

    void SetupSlotGrid() {
        slotGrid = new Slot[width, height];
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).TryGetComponent(out Slot s)) {
                if (slotGrid[s.x, s.y] != null) {
                    Debug.LogError("Two slots have same coordinates");
                }
                slotGrid[s.x, s.y] = s;
            }
        }
    }

    public Slot SelectSlotRelative(Slot origin, Vector2Int relativePos) {
        int targetX = origin.x + relativePos.x;
        int targetY = origin.y + relativePos.y;

        if (targetX > width - 1 || targetX < 0 || targetY > height - 1 || targetY < 0) {
            return null;
        }

        return slotGrid[targetX, targetY];
    }
    
    // Returns slot using absolute position.
    public Slot SelectSlot(Vector2Int pos, bool flip) {
        if (pos.x > width - 1 || pos.x < 0 || pos.y > height - 1 || pos.y < 0) {
            return null;
        }

        return slotGrid[pos.x, pos.y];
    }
}