using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;

public class GameController : MonoBehaviour
{
    public static int curWaveInd = 0;

    private static GameController instance = null;
    public static GameController Instance { get => instance; }

    public delegate void GameControllerEvent();
    public GameControllerEvent MoneyAmountChanged;
    public GameControllerEvent NewOrderAppeared;

    public List<Order> orders = null;

    public List<OrderWave> orderWaves = null;

    public PlayerController playerController = null;

    public IngredientSpriteHolder ingredientSpriteHolder = null;

    private static int coinAmount;

    private Order activeOrder;

    private Order[] activeOrders=new Order[2]; //this value should be changed when order is finished

    private GameCanvas gameCanvas = null;

    private int orderInd = 0;
    public static int CoinAmount { get => coinAmount; set => coinAmount = value; }
    public GameCanvas GameCanvas { get => gameCanvas; set => gameCanvas = value; }
    public Order ActiveOrder { get => activeOrder; set => activeOrder = value; }
    public Order[] ActiveOrders { get => activeOrders; set => activeOrders = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        activeOrders[0] = GetNextOrder();
        activeOrders[1] = GetNextOrder();
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

    public Order GetNextOrder()
    {
        if(orderInd>= orderWaves[curWaveInd].orders.Count)
        {
            Debug.Log("Orders done in wave "+curWaveInd);
            return null;
        }
        
        Order order = null;
        foreach (Order o in orders)
        {
            if(o.orderID== orderWaves[curWaveInd].orders[orderInd])
            {
                order = o;
                orderInd++;
                break;
            }
        }
        return order;
    }
    public bool DoesPlateMatchOrder(Plate plate) //maybe this could return the order index so we can remove it
    {
        if (orders[0].DoesPlateMatchesTheOrder(plate))
        {
            orders.RemoveAt(0);
            if (orders.Count == 0)
            {
                Debug.Log("Level over");
                return true;
            } else activeOrder=orders[0];
          //  gameCanvas.orderUI.SetOrderUIBasedOnOrder(orders[0]);            
            return true;
        }
        return false;
    }
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

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        SaveData saveData = new SaveData();
        SaveSystem.SaveGameXML(saveData);
    }
#endif

#if !UNITY_EDITOR
    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus)
        {
            SaveData saveData = new SaveData();
            SaveSystem.SaveGameXML(saveData);
        }
    }
#endif
}
