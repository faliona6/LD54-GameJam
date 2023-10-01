using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food {
    public class Ingredient : MonoBehaviour {
        public SO_Ingredients ingredientData;

        public int cookTime;
        public bool cooked;

        Dictionary<FoodFlavors, int> flavorScore; // i.e. {Savory: 1, Sweet: 2 }

        public List<Vector2Int> shape;
        public Vector2Int center;

        SpriteRenderer sr;

        void Awake() {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start() {
            cookTime = ingredientData.cookTime;
            shape = ingredientData.shape;
            center = ingredientData.center;
        }

        void Copy(Ingredient ingredient) {
            ingredientData = ingredient.ingredientData;
            Start();
        }

        public void setCooked() { cooked = true; }
    }
}