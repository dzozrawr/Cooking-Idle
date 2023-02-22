using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnifeChopper : MonoBehaviour
{
    public GameObject knifeModel=null;
    public Transform knifeLowPoint=null, knifeHighPoint=null;

    private Sequence tweenSequence=null;

    private void Start() {
        tweenSequence=DOTween.Sequence();

        //Tween lowToHigh= 

        tweenSequence.Append(knifeModel.transform.DOMove(knifeHighPoint.position,0.25f));
        tweenSequence.Append(knifeModel.transform.DOMove(knifeLowPoint.position,0.25f));
        tweenSequence.SetLoops(-1);   
        tweenSequence.Pause();     
    }


    public void ToggleChoppingPlay(bool shouldPlay){
        if(shouldPlay){
            knifeModel.transform.position=knifeLowPoint.position;
            knifeModel.SetActive(true);
            tweenSequence.Play();
        }else{            
            tweenSequence.Pause();
            knifeModel.SetActive(false);
        }
    }
}
