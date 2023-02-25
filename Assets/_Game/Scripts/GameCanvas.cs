using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ingredients;
using DG.Tweening;

public class GameCanvas : MonoBehaviour
{
    [Serializable]
    public class OrderUI
    {
        public GameObject orderParent = null;
        public Image foodImg;
        public IngredientUI[] ingredientsUI;
        //   public Image[] ingredientsImg = new Image[3];
        //private Ingredients.IngredientType[] ingredientsTypes = new IngredientType[3];

        private bool isOrderSet = false;

        public void SetOrderUIBasedOnOrder(Order order)
        {
            IngredientSpriteHolder ingredientSpriteHolder = GameController.Instance.ingredientSpriteHolder;
            foodImg.sprite = order.orderSprite;

            for (int i = 0; i < ingredientsUI.Length; i++)
            {
                ingredientsUI[i].HideCorrectness();
                if (i < order.ingredientList.Count)
                {
                    ingredientsUI[i].ingredientImg.sprite = ingredientSpriteHolder.GetIngredientSprite(order.ingredientList[i]);
                    ingredientsUI[i].ingredientImg.gameObject.SetActive(true);
                    //ingredientsImg[i].sprite = ingredientSpriteHolder.GetIngredientSprite(order.ingredientList[i]);
                    // ingredientsImg[i].gameObject.SetActive(true);

                }
                else
                {
                    ingredientsUI[i].ingredientImg.gameObject.SetActive(false);
                    //ingredientsImg[i].gameObject.SetActive(false);
                }
            }
            /*            for (int i = 0; i < order.ingredientList.Count; i++)    //shouldn't be longer than 3 ingredients
                        {
                            ingredientsImg[i].sprite = ingredientSpriteHolder.GetIngredientSprite(order.ingredientList[i]);
                        }  */
        }

        public void ShowOrderCorrectness(bool[] ingredientStates)
        {
            bool isAnyIncorrect = false;
            for (int i = 0; i < ingredientsUI.Length; i++)
            {
                if (ingredientsUI[i].ingredientImg.gameObject.activeSelf)
                {
                    if (!ingredientStates[i]) isAnyIncorrect = true;
                    ingredientsUI[i].ShowCorrectness(ingredientStates[i]);
                }
            }

            if (isAnyIncorrect) GameController.Instance.GameCanvas.Invoke(nameof(HideOrderCorrectness), 1.5f);
        }

        public void HideOrderCorrectness()
        {
            for (int i = 0; i < ingredientsUI.Length; i++)
            {
                if (ingredientsUI[i].ingredientImg.gameObject.activeSelf)
                {
                    ingredientsUI[i].HideCorrectness();
                }
            }
        }
    }
    public TMP_Text coinAmountTxt = null;
    public OrderUI orderUI = null;
    public Transform orderHidePlace = null;
    private Vector3 orderDefaultPosition;


    private GameController gameController = null;
    //    private Coroutine addMoneyInSequenceCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        orderDefaultPosition = orderUI.orderParent.transform.position;
        gameController = GameController.Instance;
        gameController.GameCanvas = this;

        coinAmountTxt.text = GameController.CoinAmount + "";
        gameController.MoneyAmountChanged += OnMoneyAmountChanged;

        orderUI.SetOrderUIBasedOnOrder(gameController.orders[0]);
        //        gameController.MoneyAmountChangedInc += OnMoneyAmountChangedIncrementally;
    }

    public void HideOrderCorrectness()
    {
        orderUI.HideOrderCorrectness();
    }

    public void ChangeToNextOrder()
    {
        orderUI.orderParent.transform.DOMove(orderHidePlace.position, 0.33f).OnComplete(() =>
        {
            orderUI.SetOrderUIBasedOnOrder(gameController.orders[0]);
            orderUI.orderParent.transform.DOMove(orderDefaultPosition, 0.33f);
        });
    }

    public void OnMoneyAmountChanged()
    {
        coinAmountTxt.text = GameController.CoinAmount + "";
        //  SoundManager.Instance.PlaySound("coinClaim");   //or maybe without sound?
    }
}
