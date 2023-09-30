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
        public List<Slot> mySlots = new List<Slot>();
        

        // Update is called once per frame.
        void Update()
        {
        }

        // Start cooking an ingredient.
        public void StartCooking(Ingredient ingredientToCook)
        {
        }

        // Finish cooking and remove the ingredient from the pan.
        private void FinishCooking()
        {
        }
    }
}

