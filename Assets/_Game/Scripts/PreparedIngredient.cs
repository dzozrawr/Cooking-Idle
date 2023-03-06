using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoldableNameSpace;

namespace Ingredients
{
    public enum IngredientType
    {
        Tomato, Lettuce, Patty, BurgerBun, Rice, Broccoli, Steak, TurkeyHam, Bread, Carrot, MeatWBone, Maccaroni, Cheese, SunnyEgg, FrenchFries, Bacon
    }
    public class PreparedIngredient : HoldableObject
    {
        public IngredientType ingrType;
    }
}
