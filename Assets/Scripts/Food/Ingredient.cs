using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food {
    public class Ingredient : MonoBehaviour {
        public SO_Ingredients ingredientData;

        public int cookTime;
        public bool cooked;

        public int salty;
        public int sweet;
        public int sour;
        
        public List<Vector2Int> shape;
        public Vector2Int center;

        SpriteRenderer sr;

        void Awake() {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        void Start() {
            cookTime = ingredientData.cookTime;
            shape = ingredientData.shape;
            center = ingredientData.center;

            salty = ingredientData.salty;
            sweet = ingredientData.sweet;
            sour = ingredientData.sour;
        }

        void Copy(Ingredient ingredient) {
            ingredientData = ingredient.ingredientData;
            Start();
        }
    }
}