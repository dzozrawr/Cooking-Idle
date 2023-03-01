using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoldableNameSpace;

namespace Ingredients
{
    public enum IngredientType
    {
        Tomato, Lettuce, Patty, BurgerBun, Rice, Broccoli, Steak, TurkeyHam
    }
    public class PreparedIngredient : HoldableObject
    {
        public IngredientType ingrType;
    }
}
