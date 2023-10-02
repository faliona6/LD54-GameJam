using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food
{
    [CreateAssetMenu(menuName = "Food/Augment")]
    public class SO_Augments : ScriptableObject
    {
        public string name;
        public int cookTimeModifier;
        public int burnTimeModifier;

        public int saltyModifier;
        public int sweetModifier;
        public int sourModifier;
    }
}
