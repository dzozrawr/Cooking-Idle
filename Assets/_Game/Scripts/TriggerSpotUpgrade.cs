using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayerNamespace;
public class TriggerSpotUpgrade : MonoBehaviour
{
    public Image radialProgressImg = null;

    public Canvas upgradeCanvas = null;

    private float triggerTime = 0.75f;
    private float triggerTimer = 0f;
    private int moneyInt = -1;
    private GameController gameController = null;
    private PlayerController playerController = null;

    private bool isTriggering = false;
    private bool isBought = false;
    // Start is called before the first frame update
    void Start()
    {
        upgradeCanvas.enabled = false;

        gameController = GameController.Instance;
        playerController = gameController.playerController;
    }

    private void Update()
    {
        //   if (isBought) return;
        if (isTriggering)
        {
            triggerTimer += Time.deltaTime;
            if (triggerTimer > triggerTime)
            {
                triggerTimer = triggerTime;
                upgradeCanvas.enabled = true;
                //  Invoke(nameof(DisappearAfterDelay), 0.33f);
                // isBought = true;
            }
            radialProgressImg.fillAmount = triggerTimer / triggerTime;
        }
    }

    /*    private void DisappearAfterDelay()
        {
            Sequence tweenSequence = DOTween.Sequence();

            Vector3 objectToUnlockDefaultScale = objectToUnlock.transform.localScale;
            objectToUnlock.transform.localScale = Vector3.zero;
            objectToUnlock.SetActive(true);

            tweenSequence.Append(transform.DOScale(Vector3.zero, 0.25f));
            tweenSequence.AppendInterval(0.05f);
            tweenSequence.Append(objectToUnlock.transform.DOScale(objectToUnlockDefaultScale, 0.25f));

            tweenSequence.AppendCallback(() =>
            {
                Destroy(gameObject);
            });
        }*/

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("OnTriggerEnter");
        if (other.gameObject == playerController.gameObject)
        {

            isTriggering = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject)
        {
            isTriggering = false;
            upgradeCanvas.enabled = false;
            radialProgressImg.fillAmount = 0f;
            triggerTimer = 0f;
        }
    }
}