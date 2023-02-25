using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    public Image ingredientCorrectnessImg = null;
    public Image ingredientImg = null;

    public Color correctColor, incorrectColor;

    public void ShowCorrectness(bool isCorrect)
    {
        if (isCorrect) ingredientCorrectnessImg.color = correctColor;
        else ingredientCorrectnessImg.color = incorrectColor;
    }

    public void HideCorrectness()
    {
        ingredientCorrectnessImg.color = new Color(0f, 0f, 0f, 0f);
    }
}
