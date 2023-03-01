using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanFryableIngredient : FreshIngredient
{
    public Renderer renderer = null;
    public Color cookedColor;

    public Transform jumpHighPos = null;
    public Transform ingredientParentTransform = null;

    protected Material mat = null;
    protected Color startColor;

    protected bool firstCookingEffectEntry = true;

    protected Vector3 ingredientDefaultPos;

    protected Sequence jumpSequence, rotateSequence;

    private void Awake()
    {
        mat=renderer.material;
        startColor = mat.color;

       // ingredientParentTransform = renderer.transform;
        if(ingredientParentTransform!=null)
        ingredientDefaultPos = ingredientParentTransform.localPosition;

        InitTween();
    }

    protected virtual void InitTween()
    {

    }


    public virtual void CookingEffect(float progress)
    {
        if (firstCookingEffectEntry)
        {
            jumpSequence.Play();
            rotateSequence.Play();
            firstCookingEffectEntry = false;
        }
        mat.color = Color.Lerp(startColor,cookedColor,progress);
    }
}
