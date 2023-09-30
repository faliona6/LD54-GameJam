using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public SO_Ingredient ingredient;
    
    public float cookTime;
    public bool cooked;
    
    Dictionary<FoodFlavors, int> flavorScore; // i.e. {Savory: 1, Sweet: 2 }

    public GameObject prefab;

    public List<Vector2Int> shape;

    private void Start()
    {
        cookTime = ingredient.cookTime;
    }

    public void setCooked()
    {
        cooked = true;
    }    
}
