using System.Collections.Generic;
using Food;
using UnityEngine;

[CreateAssetMenu(menuName = "General/Level")]
public class SO_Level : ScriptableObject {
    public int day;
    public int numCustomers;
    public List<SO_Ingredients> endDayNewIngredients;
}