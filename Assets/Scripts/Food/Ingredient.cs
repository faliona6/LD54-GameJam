using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Food {
    public class Ingredient : MonoBehaviour {
        
        public SO_Ingredients ingredientData;
        [SerializeField] private SpriteRenderer sprite;
        
        private int cookTime;
        [HideInInspector] public bool cooked;
        [HideInInspector] public int salty;
        [HideInInspector] public int sweet;
        [HideInInspector] public int sour;
        
        [HideInInspector] public List<Vector2Int> shape;
        [HideInInspector] public Vector2Int center;

        SpriteRenderer sr;

        void Awake() {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        void Start() {
            Init();
        }

        bool calledOnce = false;
        public void Init() {
            if (!calledOnce) {
                calledOnce = true;
                cookTime = ingredientData.cookTime;
                shape = ingredientData.shape;
                center = ingredientData.center;

                salty = ingredientData.salty;
                sweet = ingredientData.sweet;
                sour = ingredientData.sour;
            } 
        }

        void Copy(Ingredient ingredient) {
            ingredientData = ingredient.ingredientData;
            Start();
        }

        public void FinishCooking()
        {
            cooked = true;
            sprite.sprite = ingredientData.cookedSprite;
        }
    }
}