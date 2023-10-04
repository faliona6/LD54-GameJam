using Food;
using UnityEngine;

public class PantryButton : MonoBehaviour {
    public Ingredient spawnIngredient;

    void Start() {
        FlavorPanel[] fps = GetComponentsInChildren<FlavorPanel>();
        fps[0].DisplayFlavorIcon(spawnIngredient.ingredientData.salty);
        fps[1].DisplayFlavorIcon(spawnIngredient.ingredientData.sweet);
        fps[2].DisplayFlavorIcon(spawnIngredient.ingredientData.sour);
    }

    public void SpawnIngredient() {
        GameManager.Instance.ModifyMoney(-spawnIngredient.ingredientData.cost);
        Ingredient ing = Instantiate(spawnIngredient.gameObject, new Vector3(0, -20, 0), Quaternion.identity).GetComponent<Ingredient>();
        Player.Instance.PlaceInHand(ing.gameObject);
    }
}
