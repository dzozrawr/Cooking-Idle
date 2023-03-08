using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : PanFryableIngredient
{
    public override void CookingEffect(float progress)
    {
        if ((progress >= 0.5f))
        {
           // jumpSequence.Play();
           // rotateSequence.Play();
           // didSteakFlip = true;
        }

        mat.color = Color.Lerp(startColor, cookedColor, progress);
    }
}
