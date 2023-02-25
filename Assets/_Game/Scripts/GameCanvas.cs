using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [Serializable]
    public class OrderUI
    {
        public Image foodImg;
        public Image[] ingredientsImg = new Image[3];

        private bool isOrderSet = false;

        public void SetOrderUIBasedOnOrder(Order order)
        {
            IngredientSpriteHolder ingredientSpriteHolder = GameController.Instance.ingredientSpriteHolder;
            foodImg.sprite = order.orderSprite;

            for (int i = 0; i < ingredientsImg.Length; i++)
            {
                if (i < order.ingredientList.Count)
                {
                    ingredientsImg[i].sprite = ingredientSpriteHolder.GetIngredientSprite(order.ingredientList[i]);
                    ingredientsImg[i].gameObject.SetActive(true);
                }
                else
                {
                    ingredientsImg[i].gameObject.SetActive(false);
                }
            }
            /*            for (int i = 0; i < order.ingredientList.Count; i++)    //shouldn't be longer than 3 ingredients
                        {
                            ingredientsImg[i].sprite = ingredientSpriteHolder.GetIngredientSprite(order.ingredientList[i]);
                        }  */
        }
    }
    public TMP_Text coinAmountTxt = null;
    public OrderUI orderUI = null;


    private GameController gameController = null;
    //    private Coroutine addMoneyInSequenceCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        gameController.GameCanvas = this;

        coinAmountTxt.text = GameController.CoinAmount + "";
        gameController.MoneyAmountChanged += OnMoneyAmountChanged;

        orderUI.SetOrderUIBasedOnOrder(gameController.orders[0]);
        //        gameController.MoneyAmountChangedInc += OnMoneyAmountChangedIncrementally;
    }

    public void OnMoneyAmountChanged()
    {
        coinAmountTxt.text = GameController.CoinAmount + "";
        //  SoundManager.Instance.PlaySound("coinClaim");   //or maybe without sound?
    }
}
