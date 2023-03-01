using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Patty : PanFryableIngredient
{

    protected override void InitTween()
    {
        jumpSequence = DOTween.Sequence();
        jumpSequence.Append(ingredientParentTransform.DOLocalMove(jumpHighPos.position, 0.33f));
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


}
