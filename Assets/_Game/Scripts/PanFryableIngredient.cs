using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanFryableIngredient : FreshIngredient
{
    public Renderer renderer = null;
    public Color cookedColor;

    public Transform pattyHighPos = null;


    private Material mat = null;
    private Color startColor;
    public Transform ingredientParentTransform = null;

    private bool firstCookingEffectEntry = true;

    

    private Vector3 ingredientDefaultPos;

    private Sequence jumpSequence, rotateSequence;

    private void Awake()
    {
        mat=renderer.material;
        startColor = mat.color;

       // ingredientParentTransform = renderer.transform;
        ingredientDefaultPos = ingredientParentTransform.localPosition;

        jumpSequence = DOTween.Sequence();
        jumpSequence.Append(ingredientParentTransform.DOLocalMove(pattyHighPos.position, 0.33f));
        jumpSequence.Append(ingredientParentTransform.DOLocalMove(ingredientDefaultPos, 0.33f));
        jumpSequence.AppendInterval(0.33f);
        jumpSequence.SetLoops(-1);
        jumpSequence.Pause();

        rotateSequence = DOTween.Sequence();
        // rotateSequence.Append(ingredientTransform.DOBlendableLocalRotateBy(new Vector3(0, 0, 270), 0.33f));
        // rotateSequence.Append(ingredientTransform.DOBlendableLocalRotateBy(new Vector3(0, 0, 270), 0.33f));

        rotateSequence.Append(ingredientParentTransform.DOBlendableRotateBy(new Vector3(0, 0, 180), 0.22f));
        rotateSequence.Append(ingredientParentTransform.DOBlendableRotateBy(new Vector3(0, 0, 90), 0.11f));
        rotateSequence.Append(ingredientParentTransform.DOBlendableRotateBy(new Vector3(0, 0, 180), 0.22f));
        rotateSequence.Append(ingredientParentTransform.DOBlendableRotateBy(new Vector3(0, 0, 90), 0.11f));

        rotateSequence.AppendInterval(0.33f);
        rotateSequence.SetLoops(-1);
        rotateSequence.Pause();
    }


    public void CookingEffect(float progress)
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
