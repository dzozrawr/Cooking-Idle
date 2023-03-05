using PlayerNamespace;
using HoldableNameSpace;
using Ingredients;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSpot : MonoBehaviour
{
    public CoinUIEarnScript coinUIEarnScript = null;

    public CustomerManager customerManager=null;

    private PlayerController playerController = null;
    private Plate plate = null;
    private Order order = null;
    private bool doesPlateMatchOrder = false;

    private GameController gameController = null;

    private void Start()
    {
        gameController = GameController.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (plate != null) return;
        if (other.gameObject.tag.Equals("Player"))
        {//if player collides
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.PlayerState == PlayerStates.Holding)
            {
                if (playerController.HeldObject.type == HoldableType.Plate)
                {
                    plate = (Plate)playerController.HeldObject;

                    //  doesPlateMatchOrder = gameController.DoesPlateMatchAnyOrder(plate);
                    doesPlateMatchOrder = gameController.DoesPlateMatchActiveOrders(plate);

                    plate.transform.position = transform.position;
                    plate.transform.SetParent(null);
                    plate.transform.rotation = Quaternion.identity;

                    playerController.SetHoldableObject(null);

                    Invoke(nameof(PlateMatchAfterDelay), 0.85f);
                    playerController.SuccesfulTrigger(transform);
                }
            }
        }
    }

    private void PlateMatchAfterDelay()
    {

        if (doesPlateMatchOrder)
        {
            //remove the order
            //play the money tween
            //add money
            // GameObject c=  Instantiate(coinUIEarnScript.gameObject);
            //   c.transform.position = transform.position;
            // c.GetComponent<CoinUIEarnScript>().PlayCoinEarnAnimation(5);
            coinUIEarnScript.PlayCoinEarnAnimation(5);
            customerManager.FirstCustomer.PlayPosOrNegParticle(true);
            customerManager.OrderDone();
            gameController.gameCanvas.ChangeToNextOrder();
            //gameController.GameCanvas.orderUI.SetOrderUIBasedOnOrder(gameController.orders[0]);
            //  Debug.Log("Plate matches the order!");
        }
        else
        {
            coinUIEarnScript.PlayError();
            customerManager.FirstCustomer.PlayPosOrNegParticle(false);
            //play the error tween
          //  Debug.Log("Plate does NOT match the order!");
        }
        Destroy(plate.gameObject);
        plate = null;
    }


}
