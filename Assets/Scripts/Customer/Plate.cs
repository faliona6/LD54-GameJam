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
            Init(container);
        }

        bool calledOnce = false;
        public void Init(Container _container = null) {
            if (!calledOnce) {
                calledOnce = true;
                if (_container == null) _container = container;
                slotGrid.Init(container);
            } 
        }


        public void Score(List<Ingredient> ingredients)
        {

        }
    }
}
