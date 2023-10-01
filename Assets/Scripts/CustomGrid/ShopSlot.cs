using System.Collections;
using System.Collections.Generic;
using Food;
using UnityEngine;

public class ShopSlot : Slot {
    // public int price;
    public int remainingQuantity;
    public bool infiniteQuantity;

    void Start() {
        if (Ingredient != null) {
            Restock();
        }
    }

    public override Transform PickUpHeld() {
        // if (GameManager.Instance.Money < price) return null;
        // GameManager.Instance.ModifyMoney(-price);

        Transform ret = base.PickUpHeld();
        if (ret == null) return null;

        Restock();

        return ret;
    }

    void Restock() {
        if (infiniteQuantity || remainingQuantity > 0) {
            SpawnIngredient(Ingredient);

            if (remainingQuantity > 0) remainingQuantity--;
        } else {
            canPlace = false;
            canPickUp = false;
        }
    }

    public void SpawnIngredient(Ingredient ingredient) {
        if (!IsEmpty()) return;

        Ingredient ing = Factory.Instance.CreateIngredientObj(ingredient, transform.position);
        Place(ing);
    }
}