using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;
using Food;

public class Pantry : MonoBehaviour
{
    public Container container;

    List<GameObject> _ingredientPrefabs = new();

    void Start() {
        // _slotGrid = GetComponent<SlotGrid>();
        // _slotGrid.Init(container);

        // steps for placing a ingredient from creation into grid
        // Vector2Int p = new Vector2Int(2, 2);
        // Ingredient ing = Instantiate(ingredientPrefabs[0], _slotGrid.slotGrid[p].transform.position, Quaternion.identity).GetComponent<Ingredient>();
        // ing.Init();
        // ShopSlot ss = (ShopSlot) _slotGrid.slotGrid[p];
        // print(ss.Place(ing));
        // print(ss.Ingredient);
        
        // foreach (KeyValuePair<Vector2Int, Slot> pos in _slotGrid.slotGrid) {
        //     pos.Value.canPlace = false;
        // }
    }

    public void SpawnIngredient(Ingredient ingredient) {
        GameManager.Instance.ModifyMoney(-ingredient.ingredientData.cost);
        Ingredient ing = Instantiate(ingredient.gameObject, transform.position, Quaternion.identity).GetComponent<Ingredient>();
        Player.Instance.PlaceInHand(ing.gameObject);
    }
}
