using System.Collections;
using System.Collections.Generic;
using HoldableNameSpace;
using UnityEngine;

public class CookingTool : MonoBehaviour
{
    public Transform placeForIngredient = null;

    public ProgressCircle progressCircle = null;

    protected HoldableObject ingredient = null, preparedIngredient = null;

    public HoldableObject Ingredient { get => ingredient; set => ingredient = value; }
    public HoldableObject PreparedIngredient { get => preparedIngredient; set => preparedIngredient = value; }
}
