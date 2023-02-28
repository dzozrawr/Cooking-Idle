using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanFryableIngredient : FreshIngredient
{
    public Renderer renderer = null;
    public Color cookedColor;

    private Material mat = null;
    private Color startColor;

    private void Awake()
    {
        mat=renderer.material;
        startColor = mat.color;
    }
    public void CookingEffect(float progress)
    {
        mat.color = Color.Lerp(startColor,cookedColor,progress);
    }
}
