using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{

    public List<Customer> customers = null;
    public Transform orderPlace = null;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Customer c in customers)
        {
            c.Move(orderPlace, this);
        }
    }

    public void ReachedTarget()
    {
        foreach (Customer c in customers)
        {
            c.Move(null, this); //this stops all the customers
        }
    }
}
