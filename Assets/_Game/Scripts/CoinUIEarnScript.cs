using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Aezakmi.Tweens;
using DG.Tweening;

public class CoinUIEarnScript : MonoBehaviour
{
    public GameObject coinTextGO = null;
    public GameObject errorGO = null;

    private Vector3 coinTextStartingPos;
    private Vector3 errorStartingPos;

    private int moneyAmount = 2;

    private TMP_Text coinText = null;
    private TweenBase coinTweener = null;

    private CanvasGroup coinTextCanvasGroup = null;

    private TweenBase errorTweener = null;
    private CanvasGroup errorCanvasGroup = null;

    private float animationDuration = 0f;

    private Canvas canvas = null;

    private Coroutine fadeInCoroutine=null;


    private void Awake()
    {
        coinText = coinTextGO.GetComponent<TMP_Text>();
        coinTweener = coinTextGO.GetComponent<TweenBase>();
        coinTextCanvasGroup = coinTextGO.GetComponent<CanvasGroup>();


        coinTextStartingPos = coinTextGO.transform.position;


        errorTweener = errorGO.GetComponent<TweenBase>();
        errorCanvasGroup = errorGO.GetComponent<CanvasGroup>();

        errorStartingPos= errorGO.transform.position;

        canvas = GetComponent<Canvas>();


        animationDuration = coinTweener.LoopDuration;

        coinText.text = moneyAmount + "";
    }

    public void SetMoneyAmount(int amount)
    {
        moneyAmount = amount;
        coinText.text ="+" + moneyAmount;
    }

    IEnumerator FadeInTween(CanvasGroup cg, float duration)
    {
        float t = 0f;

        while (t <= duration)
        {
            cg.alpha = t / duration;
            yield return null;
            t += Time.deltaTime;
        }

        cg.alpha = 1;
        SetToDefaultState();
        fadeInCoroutine=null;
    }

    private void SetToDefaultState()
    {
        coinTextCanvasGroup.alpha = 0f;
        coinTextGO.transform.position = coinTextStartingPos;

        errorCanvasGroup.alpha = 0f;
        errorGO.transform.position = errorStartingPos;
    }

    public void PlayCoinEarnAnimation()
    {
        // transform.SetParent(transform.parent.parent);
        if(fadeInCoroutine!=null){
            StopCoroutine(fadeInCoroutine);
            fadeInCoroutine=null;
            SetToDefaultState();
            coinTweener.Tweener.Pause();
            coinTweener.Tweener.Kill();
        }

        coinText.text = "+" + moneyAmount;

        canvas.enabled = true;
        coinTweener.AddDelegateOnComplete(() =>
        {

        });
        coinTweener.PlayTween();
        fadeInCoroutine=StartCoroutine(FadeInTween(coinTextCanvasGroup, animationDuration));
    }

    public void PlayError()
    {
        //coinText.text = "+" + coinAmount;

        canvas.enabled = true;
        errorTweener.AddDelegateOnComplete(() =>
        {
            // GameController.Instance.AddMoneyIncrementally(coinAmount);
            //Destroy(gameObject);
            canvas.enabled = false;
            //tweener.RemoveDelegateOnComplete()
        });
        errorTweener.PlayTween();
        StartCoroutine(FadeInTween(errorCanvasGroup, animationDuration));
    }

    public void PlayCoinEarnAnimation(int coinAmount)
    {
      //  transform.SetParent(transform.parent.parent);

        coinText.text = "+" + coinAmount;

        canvas.enabled = true;
        coinTweener.AddDelegateOnComplete(() =>
        {
            // GameController.Instance.AddMoneyIncrementally(coinAmount);
            //Destroy(gameObject);
            canvas.enabled = false;
            GameController.Instance.AddMoney(coinAmount);
            //tweener.RemoveDelegateOnComplete()
        });
        coinTweener.PlayTween();
        StartCoroutine(FadeInTween(coinTextCanvasGroup, animationDuration));
    }
}
