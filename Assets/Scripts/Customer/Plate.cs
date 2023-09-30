using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomGrid;

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

        public void Scoring()
        {

        }
        public override string ToString()
        {
            return $"Container: {container}, SlotGrid: {slotGrid}";
        }
    }
}
