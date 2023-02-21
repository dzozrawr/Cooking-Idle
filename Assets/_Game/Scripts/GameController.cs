using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;

    public List<Order> orders = null;

    public static GameController Instance { get => instance; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public bool DoesPlateMatchAnyOrder(Plate plate) //maybe this could return the order index so we can remove it
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].DoesPlateMatchesTheOrder(plate))
            {
                orders.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
}
