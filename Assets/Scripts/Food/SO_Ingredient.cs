using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Food/SO_Ingredient")]
public class SO_Ingredient : ScriptableObject
{
    public string name;
    public FoodType FoodType;
    public int cookTime;
    public List<Vector2Int> shape;
}

public enum FoodType
{
    Sweet,
    Savory,
    Sour,
}
