using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pan
{ 
    public class Pan : MonoBehaviour
    {
        public bool isCooking = false; // Indicates whether the pan is currently cooking an ingredient.
        public float cookTime = 5f; // The time it takes to cook an ingredient (in seconds).
        

        private float currentCookTimer = 0f; // Timer to track the cooking progress.

        // Update is called once per frame.
        void Update()
        {
            // Check if the pan is cooking an ingredient.
            if (isCooking)
            {
                // Increment the cooking timer.
                currentCookTimer += Time.deltaTime;

                // Check if the cooking time is complete.
                if (currentCookTimer >= cookTime)
                {
                    FinishCooking();
                }
            }
        }

        // Start cooking an ingredient.
        public void StartCooking(Ingredient ingredientToCook)
        {
            if (!isCooking)
            {
                isCooking = true;
                currentIngredient = ingredientToCook;
                Debug.Log("Started cooking " + ingredientToCook.name + " in the pan.");
            }
        }

        // Finish cooking and remove the ingredient from the pan.
        private void FinishCooking()
        {
            isCooking = false;
            currentCookTimer = 0f;
        }
    }
}

