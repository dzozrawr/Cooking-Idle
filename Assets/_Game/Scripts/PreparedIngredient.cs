using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoldableNameSpace;

namespace Ingredients
{
    public enum IngredientType
    {
        Tomato, Lettuce
    }
    public class PreparedIngredient : HoldableObject
    {
        public IngredientType ingrType;
    }
}
