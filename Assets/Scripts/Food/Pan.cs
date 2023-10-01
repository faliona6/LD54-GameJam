using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;
using UnityEngine.UI;

namespace Food
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    
    public class Pan : MonoBehaviour
    {
        public bool isCooking = false; // Indicates whether the pan is currently cooking.

        public Container container; // Reference to the container associated with the pan.
        public List<Slot> panSlots; // Reference to the slot grid associated with the pan.

        public List<Transform> knobPositions = new List<Transform>();
        
        [SerializeField] private Button cookButton;
     
        public PanManager panManager;
        public void Start()
        {
            cookButton = panManager.cookButtons[panManager.panCount - 1];
            cookButton.gameObject.SetActive(true);
            
            cookButton.onClick.AddListener(StartCooking);
        }
        // Start cooking all ingredients in the pan
        public void StartCooking()
        {
            if (isCooking) return;

            // Start cooking.
            isCooking = true;   

            // Get all the ingredients in your pan slot references.
            List<Ingredient> ingredientsToCook = GetIngredientsToCook();

            float cookTime = 0f;

            // each ingredient increases the total cook time for the pan
            foreach (Ingredient ingredient in ingredientsToCook)
            {
                cookTime += ingredient.cookTime;
            }

            Debug.Log("Cooking started.");
            // Start a coroutine to handle the cooking timer.
            StartCoroutine(CookingTimer(cookTime, ingredientsToCook));
        }

        // Finish cooking and set each ingredient to be cooked = true
        private void FinishCooking(List<Ingredient> ingredientsInPan)
        {
            isCooking = false;
            foreach (Ingredient ingredient in ingredientsInPan)
            {
                ingredient.cooked = true;
            }
            Debug.Log("Cooking finished.");
        }

        private IEnumerator CookingTimer(float totalCookTime, List<Ingredient> ingredientsInPan)
        {
            float timer = 0f;

            while (timer < totalCookTime)
            {
                // Increment the timer.
                timer += Time.deltaTime;

                // You can update a progress bar or display the remaining time if needed.

                yield return null; // Wait for the next frame.
            }

            // Cooking is complete. Finish cooking and set ingredients to be cooked = true.
            FinishCooking(ingredientsInPan);
        }

        // Method to retrieve ingredients from slots.
        private List<Ingredient> GetIngredientsToCook()
        {
            List<Ingredient> ingredientsToCook = new List<Ingredient>();

            foreach (Slot slot in panSlots)
            {
                // Check if the slot's ingredient is not already in the list before adding.
                if (slot.Ingredient != null && !ingredientsToCook.Contains(slot.Ingredient))
                {
                    ingredientsToCook.Add(slot.Ingredient);
                }
            }

            return ingredientsToCook;
        }
    }
}