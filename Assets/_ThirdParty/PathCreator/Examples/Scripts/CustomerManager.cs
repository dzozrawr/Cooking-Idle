using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{

    public List<Customer> customers = null;
    public Transform orderPlace = null;

    public Transform customerSpawnPlace = null;

    public List<GameObject> customerPrefabs = null;

    public Transform leaveTarget = null;

    private Customer firstCustomer = null;

    private int maxCustomerN = 5;

    public Customer FirstCustomer { get => firstCustomer; set => firstCustomer = value; }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Customer c in customers)
        {
            c.Move(orderPlace, this);
        }
    }

    public void ReachedTarget(Customer customer)
    {
        firstCustomer = customer;
        foreach (Customer c in customers)
        {
            c.Move(null, this); //this stops all the customers
        }

        if (customers.Count < maxCustomerN)
        {
            GameObject newCustomerGO = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], customerSpawnPlace.position, customers[0].transform.rotation);
            newCustomerGO.transform.SetParent(transform);

            Customer c = newCustomerGO.GetComponent<Customer>();

            SubscribeCustomer(c);

            c.FadeIn(1f);
        }
    }

    public void UnsubscribeCustomer(Customer c)
    {
        customers.Remove(c);
    }

    public void SubscribeCustomer(Customer c)
    {
        customers.Add(c);
    }

    public void OrderDone()
    {
        UnsubscribeCustomer(firstCustomer);


        float fadeOutStartDelay = 3f;
        float fadeOutDuration = 1f;
        firstCustomer.MoveAndDie(leaveTarget, this, fadeOutStartDelay, fadeOutDuration);
        Destroy(firstCustomer.gameObject, fadeOutStartDelay + fadeOutDuration + 0.5f);
        //Destroy(firstCustomer.gameObject);//change this to customer leaving and then destroying
        firstCustomer = null;

        foreach (Customer c in customers)
        {
            c.Move(orderPlace, this);
        }    //here somewhere spawn another customer
    }
}
