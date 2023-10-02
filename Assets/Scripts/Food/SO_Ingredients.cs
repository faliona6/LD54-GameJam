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
        public int salty;
        public int sweet;
        public int sour;
        
        public int cookTime;
        public List<Vector2Int> shape;
        public Vector2Int center;
        
        public Sprite uncookedSprite;
        public Sprite cookedSprite;

        public int cost;
    }
}
