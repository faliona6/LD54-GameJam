using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;
using Food;

namespace Customer
{
    public class Plate : MonoBehaviour
    {
        public Container container;
        public SlotGrid slotGrid;

        private void Start()
        {
            slotGrid.Init(container);
        }

        public void Score(List<Ingredient> ingredients)
        {

        }
    }
}
