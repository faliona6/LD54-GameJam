using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public SO_Ingredient ingredient;
    
    public int cookTime;
    public bool cooked;

    public List<Vector2Int> shape;

    private void Start()
    {
        cookTime = ingredient.cookTime;
    }

    public void Place()
    {
        
    }

    public void PickUp()
    {
        
    }
}
