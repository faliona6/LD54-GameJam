using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/SO_Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public string name;
    public FoodType FoodType;
    Dictionary<FoodFlavors, int> intitialFlavorScore;
    public int cookTime;
    public List<Vector2Int> shape;
}

public enum FoodType
{
    Meat,
    Vegetable,
}
public enum FoodFlavors
{
    Sweet,
    Savory
}
