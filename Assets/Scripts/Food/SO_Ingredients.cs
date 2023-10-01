using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;

namespace Food
{
    [CreateAssetMenu(menuName = "Food/Ingredient")]
    public class SO_Ingredients : ScriptableObject
    {
        public string name;
        public FoodType FoodType;
        Dictionary<FoodFlavors, int> intitialFlavorScore;
        public int cookTime;
        public int burnTime;
        public Container container;
    }
}
