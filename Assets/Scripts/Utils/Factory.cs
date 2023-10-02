using System.ComponentModel;
using Customer;
using Food;
using UnityEngine;

public class Factory : MonoBehaviour {
    public static Factory Instance => _instance;
    static Factory _instance;

    public GameObject _ingredientBase;
    public static GameObject IngredientBase { get; private set; }
    public GameObject _plateBase;
    public static GameObject PlateBase { get; private set; }

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }

        PlateBase = _plateBase;
        IngredientBase = _ingredientBase;
    }
    
    public Ingredient CreateIngredientObj(SO_Ingredients ingredientData, Vector2 pos) {
        Ingredient ing = Instantiate(_ingredientBase, pos, Quaternion.identity).GetComponent<Ingredient>();
        ing.ingredientData = ingredientData;

        return ing;
    }
    
    public Ingredient CreateIngredientObj(Ingredient ingredient, Vector2 pos) {
        Ingredient ing = Instantiate(_ingredientBase, pos, Quaternion.identity).GetComponent<Ingredient>();
        ing.ingredientData = ingredient.ingredientData;

        return ing;
    }
    
    // TODO:
    // public Augment CreateAugmentObj(SO_Augments ingredientData, Vector2 pos) {
    // }
    
    // TODO: wherever (prob customer generator?) stores possible plate containers and calls this func to create Plate GameObject

    // CreatePlateObj instantiates a Plate GameObject with data from plateContainer at position
    public Plate CreatePlateObj(Container plateContainer, Vector2 pos) {
        Plate p = Instantiate(_plateBase, pos, Quaternion.identity).GetComponent<Plate>();
        // TODO: call plate init code

        return p;
    }
}