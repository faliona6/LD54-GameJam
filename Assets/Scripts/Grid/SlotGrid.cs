using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SlotGrid : MonoBehaviour {
    public Dictionary<Vector2Int, Slot> slotGrid = new();

    // public int Width => _width;
    // public int Height => _height;
    // [SerializeField] int _width, _height;

    [SerializeField] GameObject _slotPrefab;

    void Awake() {
        int[,] a = new int[2, 3];
        Init(a);
    }

    void Init(int[,] shape) {
        for (int x = 0; x < shape.GetLength(0); x++) {
            for (int y = 0; y < shape.GetLength(1); y++) {
                Slot slot = Instantiate(_slotPrefab, transform).GetComponent<Slot>();
                slot.transform.localPosition = new Vector3(x, y, 0);
                
                slotGrid[new Vector2Int(x, y)] = slot;
            }
        }
    }

    public Slot SelectSlotRelative(Slot origin, Vector2Int relativePos) {
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

    public bool IsInBounds(int x, int y) {
        return slotGrid.ContainsKey(new Vector2Int(x, y));
    }
    // public bool IsInBounds(int x, int y) { return !(x < 0 || x >= _width || y < 0 || y >= _height); }
}