using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bacon : PanFryableIngredient
{

    public override void CookingEffect(float progress)
    {
        if (progress > 1f) progress = 1f;
        mat.color = Color.Lerp(startColor, cookedColor, progress);
    }
}
