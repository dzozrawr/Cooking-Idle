using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController Instance { get => instance; }

    public delegate void GameControllerEvent();
    public GameControllerEvent MoneyAmountChanged;

    public List<Order> orders = null;

    public PlayerController playerController=null;

    public IngredientSpriteHolder ingredientSpriteHolder = null;

    private static int coinAmount;

    private GameCanvas gameCanvas = null;
    public static int CoinAmount { get => coinAmount; set => coinAmount = value; }
    public GameCanvas GameCanvas { get => gameCanvas; set => gameCanvas = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            AddMoney(5);
        }
    }
#endif

    public bool DoesPlateMatchAnyOrder(Plate plate) //maybe this could return the order index so we can remove it
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].DoesPlateMatchesTheOrder(plate))
            {
                orders.RemoveAt(i);
                gameCanvas.orderUI.SetOrderUIBasedOnOrder(orders[i]);
                return true;
            }
        }
        return false;
    }

    public void AddMoney(int moneyAmountToAdd)
    {
        coinAmount += moneyAmountToAdd;
        MoneyAmountChanged?.Invoke();
    }
}
