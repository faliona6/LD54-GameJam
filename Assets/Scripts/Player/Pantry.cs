using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;
using Food;

[RequireComponent(typeof(SlotGrid))]
public class Pantry : MonoBehaviour
{
    public Container container;
    
    SlotGrid _slotGrid;

    [SerializeField] List<GameObject> ingredientPrefabs = new List<GameObject>();

    void Start() {
        _slotGrid = GetComponent<SlotGrid>();
        
        _slotGrid.Init(container);
        foreach (KeyValuePair<Vector2Int, Slot> pos in _slotGrid.slotGrid) {
            pos.Value.canPlace = false;
        }

        Ingredient ing = Instantiate(ingredientPrefabs[0], _slotGrid.slotGrid[new Vector2Int(1, 1)].transform.position, Quaternion.identity).GetComponent<Ingredient>();
        ing.Init();
        ShopSlot ss = (ShopSlot) _slotGrid.slotGrid[new Vector2Int(1, 1)];
        ss.Place(ing);
        print(ss.Ingredient);
    }
}
