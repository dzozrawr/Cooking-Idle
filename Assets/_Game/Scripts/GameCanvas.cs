using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public TMP_Text coinAmountTxt = null;

    private GameController gameController = null;
    //    private Coroutine addMoneyInSequenceCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;

        coinAmountTxt.text = GameController.CoinAmount + "";
        gameController.MoneyAmountChanged += OnMoneyAmountChanged;
        //        gameController.MoneyAmountChangedInc += OnMoneyAmountChangedIncrementally;
    }

    public void OnMoneyAmountChanged()
    {
        coinAmountTxt.text = GameController.CoinAmount + "";
        //  SoundManager.Instance.PlaySound("coinClaim");   //or maybe without sound?
    }
}
