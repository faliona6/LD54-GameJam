using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    [CreateAssetMenu(menuName = "Food/Flavor")]
    public class SO_Flavors : ScriptableObject
    {
        public string name;
        public FoodFlavors flavor;
        public int flavorModifier;
    }
}
