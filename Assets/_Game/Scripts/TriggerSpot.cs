using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayerNamespace;
using DG.Tweening;

public class TriggerSpot : MonoBehaviour
{
    public TMP_Text moneyText = null;
    public Image radialProgressImg = null;

    public GameObject objectToUnlock = null;

    private float buyingTime = 1.5f;
    private float buyingTimer = 0f;
    private int moneyInt = -1;
    private GameController gameController = null;
    private PlayerController playerController = null;

    private bool isBuying = false;
    private bool isBought = false;
    // Start is called before the first frame update
    void Start()
    {
        objectToUnlock.SetActive(false);
        if (moneyText != null)
        {
            moneyInt = int.Parse(moneyText.text);
        }

        gameController = GameController.Instance;
        playerController = gameController.playerController;
    }

    private void Update()
    {
        if (isBought) return;
        if (isBuying)
        {
            buyingTimer += Time.deltaTime;
            if (buyingTimer > buyingTime)
            {
                buyingTimer = buyingTime;
                Invoke(nameof(DisappearAfterDelay),0.33f);
                isBought=true;
            }
            radialProgressImg.fillAmount = buyingTimer / buyingTime;
        }
        else
        {
            if (radialProgressImg.fillAmount > 0f)
            {
                buyingTimer -= Time.deltaTime;
                if (buyingTimer < 0f) buyingTimer = 0f;
                radialProgressImg.fillAmount = buyingTimer / buyingTime;
            }
        }
    }

    private void DisappearAfterDelay(){
        Sequence tweenSequence=DOTween.Sequence();

        Vector3 objectToUnlockDefaultScale=objectToUnlock.transform.localScale;
        objectToUnlock.transform.localScale=Vector3.zero;
        objectToUnlock.SetActive(true);

        tweenSequence.Append(transform.DOScale(Vector3.zero,0.25f));
        tweenSequence.AppendInterval(0.05f);
        tweenSequence.Append(objectToUnlock.transform.DOScale(objectToUnlockDefaultScale,0.25f));

        tweenSequence.AppendCallback(()=>{
            Destroy(gameObject);
        });
    }

    private void OnTriggerEnter(Collider other)
    {

        if (moneyInt <= 0) return;        
        if (other.gameObject == playerController.gameObject)
        {
            if (GameController.CoinAmount >= moneyInt)
            {
                isBuying = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            isBuying = false;
        }
    }
}