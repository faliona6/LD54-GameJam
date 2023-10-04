using System.Collections.Generic;
using UnityEngine;

namespace Food {
    public class Ingredient : MonoBehaviour {
        
        public SO_Ingredients ingredientData;
        [SerializeField] private SpriteRenderer sprite;
        
        private int cookTime;
        public bool cooked;
        [HideInInspector] public bool burned;
        [HideInInspector] public int salty;
        [HideInInspector] public int sweet;
        [HideInInspector] public int sour;
        
        [HideInInspector] public List<Vector2Int> shape;
        [HideInInspector] public Vector2Int center;
        
        [HideInInspector] public int cost;

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

                cost = ingredientData.cost;
            } 
        }

        void Copy(Ingredient ingredient) {
            ingredientData = ingredient.ingredientData;
            Init();
        }

        public void FinishCooking()
        {
            cooked = true;
            sprite.sprite = ingredientData.cookedSprite;
        }
        
        public void Burn()
        {
            burned = true;
            ColorUtility.TryParseHtmlString( "#412020" , out Color burntColor);
            sprite.color = burntColor;
        }
    }
}