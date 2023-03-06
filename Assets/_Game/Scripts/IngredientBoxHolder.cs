using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingredients;

public class IngredientBoxHolder : MonoBehaviour
{
    private static IngredientBoxHolder instance = null;

    public static IngredientBoxHolder Instance { get => instance; }
    public List<IngredientBox> ingredientBoxes = null;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public IngredientBox GetIngredientBox(IngredientType type)
    {
        foreach (IngredientBox box in ingredientBoxes)
        {
            if (box.ingredientType == type)
            {
                return box;
            }
        }
        return null;
    }
}
