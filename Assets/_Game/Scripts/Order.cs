using System;
using System.Collections;
using System.Collections.Generic;
using Ingredients;
using UnityEngine;

[Serializable]
public class Order 
{
    public Sprite orderSprite=null;
    public List<IngredientType> ingredientList = null;

    public bool DoesPlateMatchesTheOrder(Plate plate)
    {
        List<IngredientType> orderIngrList = DeepCopyIngrList(ingredientList);
        bool[] orderIngrListState = new bool[3] { false, false, false };
        if (plate.Ingredients.Count == orderIngrList.Count)
        {
           
            bool isMismatch = false;
            for (int i = 0; i < plate.Ingredients.Count; i++)
            {
                for (int j = 0; j < orderIngrList.Count; j++)
                {
                    if (orderIngrListState[i]) continue;   //already found
                    if (plate.Ingredients[i].ingrType == orderIngrList[j])
                    {
                        orderIngrListState[i] = true;
                       // orderIngrList.RemoveAt(j);  //here we find the ingredient and break
                        break;
                    }
                    if ((j + 1) >= orderIngrList.Count) isMismatch = true;  //if we don't find the ingredient its a mismatch and we can abort the whole process
                }
               // if (isMismatch) break;
            }
            GameController.Instance.GameCanvas.orderUI.ShowOrderCorrectness(orderIngrListState);
            return !isMismatch;
        }
        GameController.Instance.GameCanvas.orderUI.ShowOrderCorrectness(orderIngrListState);
        return false;
    }

    private List<IngredientType> DeepCopyIngrList(List<IngredientType> copyFrom)
    {
        List<IngredientType> copyTo = new List<IngredientType>();

        foreach (IngredientType item in copyFrom)
        {
            copyTo.Add(item);
        }
        return copyTo;
    }
}
