using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnifeChopper : MonoBehaviour
{
    public GameObject knifeModel = null;
    public Transform knifeLowPoint = null, knifeHighPoint = null;

    private Sequence tweenSequence = null;

    private Sequence rotationSequence = null;

    /*     private void Start()
        {
            tweenSequence = DOTween.Sequence();

            //Tween lowToHigh= 

            tweenSequence.Append(knifeModel.transform.DOMove(knifeHighPoint.position, 0.25f));
            tweenSequence.Append(knifeModel.transform.DOMove(knifeLowPoint.position, 0.25f));
            tweenSequence.SetLoops(-1);
            tweenSequence.Pause();
        } */



    private void CreateChoppingSequence()
    {
        tweenSequence = DOTween.Sequence();

        //Tween lowToHigh= 
        tweenSequence.Append(knifeModel.transform.DOMove(knifeLowPoint.position, 0.25f));
        tweenSequence.Append(knifeModel.transform.DOMove(knifeHighPoint.position, 0.25f));

        tweenSequence.SetLoops(-1);
        tweenSequence.Pause();

        Vector3 knifeRot = knifeModel.transform.rotation.eulerAngles;
        rotationSequence = DOTween.Sequence();
        rotationSequence.Append(knifeModel.transform.DORotate(new Vector3(0, knifeRot.y, knifeRot.z), 0.25f));
        rotationSequence.Append(knifeModel.transform.DORotate(new Vector3(-30, knifeRot.y, knifeRot.z), 0.25f));

        rotationSequence.SetLoops(-1);
        rotationSequence.Pause();
    }


    public void ToggleChoppingPlay(bool shouldPlay)
    {
        if (shouldPlay)
        {
            CreateChoppingSequence();
            knifeModel.transform.position = knifeHighPoint.position;
            knifeModel.SetActive(true);
            tweenSequence.Play();
            rotationSequence.Play();
        }
        else
        {
            tweenSequence.Pause();
            rotationSequence.Pause();
            knifeModel.SetActive(false);
        }
    }
    [ContextMenu("PlayChopping")]
    public void PlayChopping()
    {

        CreateChoppingSequence();
        knifeModel.transform.position = knifeHighPoint.position;
        knifeModel.SetActive(true);
        tweenSequence.Play();
        rotationSequence.Play();


    }
}
