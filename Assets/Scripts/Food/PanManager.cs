using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Food
{
    public class PanManager : MonoBehaviour
    {
        public Slider progressBar;
        public Button cookButton;
        
        private List<Slot> _cookingSlots = new();
        private SlotGrid _slotGrid;
        private bool isCooking = false;

        void Start()
        {
            Player instance = Player.Instance;
            _slotGrid = instance.PlayerInventory.slotGrid;

            _cookingSlots = _slotGrid.GetSlotsOfType(Slot.SlotType.Cooking);
            progressBar.gameObject.SetActive(false);
        }

        public void HandleStartCooking()
        {
            if (isCooking)
            {
                return;
            }
            
            List<Ingredient> ingredients = GetIngredientsToCook();
            if (ingredients.Count == 0)
            {
                return;
            }

            cookButton.interactable = false;
            progressBar.gameObject.SetActive(true);
            isCooking = true;

            SetIngredientsLocked(ingredients, true);
            int totalCookTime = ingredients.Select(i => i.ingredientData.cookTime).Sum();

            StartCoroutine(CookingTimer((int)(totalCookTime / ingredients.Count), ingredients));
        }

        private void FinishCooking(List<Ingredient> ingredients)
        {
            ingredients.ForEach(ingredient => ingredient.FinishCooking());
            isCooking = false;
            SetIngredientsLocked(ingredients, false);
            cookButton.interactable = true;
        }
        
        private List<Ingredient> GetIngredientsToCook()
        {
            List<Ingredient> ingredientsToCook = new List<Ingredient>();

            foreach (Slot slot in _cookingSlots)
            {
                // Check if the slot's ingredient is not already in the list before adding.
                if (slot.Ingredient != null && !ingredientsToCook.Contains(slot.Ingredient))
                {
                    ingredientsToCook.Add(slot.Ingredient);
                }
            }

            return ingredientsToCook;
        }

        private void SetIngredientsLocked(List<Ingredient> ingredients, bool isLocked)
        {
            ingredients.ForEach(ingredient =>
            {
                MoveableIngredient moveableIngredient = ingredient.GetComponent<MoveableIngredient>();
                if (moveableIngredient != null)
                {
                    moveableIngredient.isLocked = isLocked;
                }
            });
        }
        
        private IEnumerator CookingTimer(float totalCookTime, List<Ingredient> ingredientsInPan)
        {
            float timer = 0f;

            while (timer < totalCookTime)
            {
                // Increment the timer.
                timer += Time.deltaTime;
                progressBar.value = timer / totalCookTime;

                // You can update a progress bar or display the remaining time if needed.

                yield return null; // Wait for the next frame.
            }

            // Cooking is complete. Finish cooking and set ingredients to be cooked = true.
            FinishCooking(ingredientsInPan);
            progressBar.gameObject.SetActive(false);
        }
    }
}