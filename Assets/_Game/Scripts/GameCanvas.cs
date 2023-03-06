using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ingredients;
using DG.Tweening;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    [Serializable]
    public class OrderUI
    {
        public GameObject orderParent = null;
        public Image foodImg;
        public TMP_Text foodNameText = null;
        public IngredientUI[] ingredientsUI;

        public Image orderBackgroundImg=null;

        public float orderUIDefaultPosX;
        //   public Image[] ingredientsImg = new Image[3];
        //private Ingredients.IngredientType[] ingredientsTypes = new IngredientType[3];

        private bool isOrderSet = false;

        private bool isAnyIngrIncorrect = false;

        public bool IsAnyIngrIncorrect { get => isAnyIngrIncorrect; set => isAnyIngrIncorrect = value; }

        public void SetOrderUIBasedOnOrder(Order order)
        {
            if (order == null)
            {
                ToggleShow(false);
                return;
            }

            IngredientSpriteHolder ingredientSpriteHolder = GameController.Instance.ingredientSpriteHolder;
            foodImg.sprite = order.orderSprite;
            foodNameText.text = order.foodName;

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
            isAnyIngrIncorrect = false;
            for (int i = 0; i < ingredientsUI.Length; i++)
            {
                if (ingredientsUI[i].ingredientImg.gameObject.activeSelf)
                {
                    if (!ingredientStates[i]) isAnyIngrIncorrect = true;
                    ingredientsUI[i].ShowCorrectness(ingredientStates[i]);
                }
            }

            if (isAnyIngrIncorrect)
            {
                GameController.Instance.gameCanvas.Invoke(nameof(HideOrderCorrectness), 1.5f);
            }

        }

        public void HideOrderCorrectness()
        {
            if (isAnyIngrIncorrect)
            {
                for (int i = 0; i < ingredientsUI.Length; i++)
                {
                    if (ingredientsUI[i].ingredientImg.gameObject.activeSelf)
                    {
                        ingredientsUI[i].HideCorrectness();
                    }
                }
                isAnyIngrIncorrect = false;
            }
        }

        public void ToggleShow(bool shouldShow)
        {
            if (shouldShow) orderParent.GetComponent<CanvasGroup>().alpha = 1; else orderParent.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
    public TMP_Text coinAmountTxt = null;
    public OrderUI orderUI = null;
    public List<OrderUI> orderUIs = null;
    public Transform orderHidePlace = null;
    public TMP_Text dayText = null;

    public Sprite orderInactiveBackground=null, orderActiveBackground=null;
    private Vector3 orderDefaultPosition;
    private int correctOrderInd = -1;


    private GameController gameController = null;

    public int CorrectOrderInd { get => correctOrderInd; set => correctOrderInd = value; }

    //    private Coroutine addMoneyInSequenceCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        foreach (OrderUI orderUI in orderUIs)
        {
            orderUI.orderUIDefaultPosX = orderUI.orderParent.transform.position.x;
        }
        //orderUI.orderUIDefaultPos
        orderDefaultPosition = orderUI.orderParent.transform.position;
        gameController = GameController.Instance;
        // gameController.GameCanvas = this;

        coinAmountTxt.text = GameController.CoinAmount + "";
        gameController.MoneyAmountChanged += OnMoneyAmountChanged;

        orderUIs[0].SetOrderUIBasedOnOrder(gameController.ActiveOrders[0]);
        orderUIs[1].SetOrderUIBasedOnOrder(gameController.ActiveOrders[1]);
        //        gameController.MoneyAmountChangedInc += OnMoneyAmountChangedIncrementally;
    }

    public void HideOrderCorrectness()
    {
        //orderUI.HideOrderCorrectness();
        orderUIs[0].HideOrderCorrectness();
        orderUIs[1].HideOrderCorrectness();
    }

    public void ChangeToNextOrder()
    {
        //Debug.Log("ChangeToNextOrder()");
        //orderUIs
        if (correctOrderInd != -1)
        {
            //   Debug.Log("if (correctOrderInd != -1)");
            orderUIs[correctOrderInd].orderUIDefaultPosX = orderUIs[correctOrderInd].orderParent.GetComponent<RectTransform>().anchoredPosition.x;
            orderUIs[correctOrderInd].orderParent.transform.DOMoveX(orderHidePlace.position.x, 0.33f).OnComplete(() =>
                        {

                            if (gameController.ShouldStartNewWave)
                            {
                                gameController.GoToNextWave();
                             //   Debug.Log("gameController.GoToNextWave();");
                                orderUIs[correctOrderInd].SetOrderUIBasedOnOrder(gameController.ActiveOrders[correctOrderInd]);

                                return;
                            }
                            if (gameController.ActiveOrders[correctOrderInd] == null)
                            {
                                gameController.NewOrderAppeared?.Invoke();
                            }

                            orderUIs[correctOrderInd].SetOrderUIBasedOnOrder(gameController.ActiveOrders[correctOrderInd]);
                            
                            orderUIs[correctOrderInd].orderParent.transform.SetAsFirstSibling();
                            orderUIs[correctOrderInd].orderBackgroundImg.sprite=orderInactiveBackground;
                            orderUIs[(correctOrderInd+1)%orderUIs.Count].orderBackgroundImg.sprite=orderActiveBackground;

                            orderUIs[correctOrderInd].orderParent.GetComponent<RectTransform>().DOAnchorPosX(orderUIs[correctOrderInd].orderUIDefaultPosX, 0.33f).OnComplete(() =>
                            {
                              //  Debug.Log("deepest tween");
                                if (correctOrderInd == 1)
                                    gameController.NewOrderAppeared?.Invoke();
                                correctOrderInd = -1;
                            });
                        });
        }
        /*        orderUI.orderParent.transform.DOMove(orderHidePlace.position, 0.33f).OnComplete(() =>
                {
                    orderUI.SetOrderUIBasedOnOrder(gameController.orders[0]);
                    orderUI.orderParent.transform.DOMove(orderDefaultPosition, 0.33f).OnComplete(() =>
                    {
                        gameController.NewOrderAppeared?.Invoke();
                        correctOrderInd = -1;
                    });
                });*/
        /*        orderUI.orderParent.transform.DOMove(orderHidePlace.position, 0.33f).OnComplete(() =>
                {
                    orderUI.SetOrderUIBasedOnOrder(gameController.orders[0]);
                    orderUI.orderParent.transform.DOMove(orderDefaultPosition, 0.33f).OnComplete(() =>
                    {
                        gameController.NewOrderAppeared?.Invoke();
                    });
                });*/
    }
    public void InitOrderUIsForNextWave()
    {
        orderUIs[0].SetOrderUIBasedOnOrder(gameController.ActiveOrders[0]);
        orderUIs[1].SetOrderUIBasedOnOrder(gameController.ActiveOrders[1]);

        orderUIs[0].orderParent.transform.SetAsLastSibling();

        orderUIs[0].orderBackgroundImg.sprite=orderActiveBackground;
        orderUIs[1].orderBackgroundImg.sprite=orderInactiveBackground;

        orderUIs[0].ToggleShow(true);
        orderUIs[1].ToggleShow(true);

        orderUIs[0].orderParent.transform.position = new Vector3(orderHidePlace.position.x, orderUIs[0].orderParent.transform.position.y, orderUIs[0].orderParent.transform.position.z);
        orderUIs[1].orderParent.transform.position = new Vector3(orderHidePlace.position.x, orderUIs[1].orderParent.transform.position.y, orderUIs[1].orderParent.transform.position.z);

        orderUIs[0].orderParent.GetComponent<RectTransform>().DOAnchorPosX(orderUIs[0].orderUIDefaultPosX, 0.33f).OnComplete(() =>
        {
            gameController.NewOrderAppeared?.Invoke();
            correctOrderInd = -1;
        });

        orderUIs[1].orderParent.GetComponent<RectTransform>().DOAnchorPosX(orderUIs[1].orderUIDefaultPosX, 0.33f).OnComplete(() =>
        {
            gameController.NewOrderAppeared?.Invoke();
            correctOrderInd = -1;
        });

        CustomerManager.Instance.SpawnNewCustomerWave();
    }
    public void UpdateDayForNextWave(int dayNumber)
    {
        Vector3 dayTextDefaultScale = dayText.transform.localScale;
        dayText.transform.DOScale(dayTextDefaultScale * 1.5f, 0.75f).OnComplete(() =>
          {
              dayText.text = "Day " + dayNumber;
              dayText.transform.DOScale(dayTextDefaultScale, 0.75f).OnComplete(() =>
              {
                  InitOrderUIsForNextWave();
              });
          });

        //maybe add an animation
    }

    public void SetDayNumber(int dayNumber)
    {
        dayText.text = "Day " + dayNumber;

        //maybe add an animation
    }

    public void OnMoneyAmountChanged()
    {
        coinAmountTxt.text = GameController.CoinAmount + "";
        //  SoundManager.Instance.PlaySound("coinClaim");   //or maybe without sound?
    }
}
