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

        // steps for placing a ingredient from creation into grid
        Vector2Int p = new Vector2Int(2, 2);
        Ingredient ing = Instantiate(ingredientPrefabs[0], _slotGrid.slotGrid[p].transform.position, Quaternion.identity).GetComponent<Ingredient>();
        ing.Init();
        ShopSlot ss = (ShopSlot) _slotGrid.slotGrid[p];
        print(ss.Place(ing));
        print(ss.Ingredient);
        
        foreach (KeyValuePair<Vector2Int, Slot> pos in _slotGrid.slotGrid) {
            pos.Value.canPlace = false;
        }
    }

    public void SpawnIngredient(SO_Ingredients ingredientData) {
        print(Factory.Instance);
        Ingredient ing = Factory.Instance.CreateIngredientObj(ingredientData, Vector3.zero);
        Player.Instance.PlaceInHand(ing.gameObject);
    }
}
