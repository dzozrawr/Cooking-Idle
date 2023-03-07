using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance = null;

    public static CustomerManager Instance { get => instance; }

    public List<Customer> customers = null;
    public Transform orderPlace = null;

    public Transform customerSpawnPlace = null;

    public List<GameObject> customerPrefabs = null;

    public Transform leaveTarget = null;

    private Customer firstCustomer = null;

    private Customer lastCustomer = null;

    private int maxCustomerN = 5;

    private GameController gameController = null;

    public Customer FirstCustomer { get => firstCustomer; set => firstCustomer = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        SpawnNewCustomerWave();
        /*         for (int i = 0; i < gameController.orderWaves[GameController.curWaveInd].orders.Count; i++)
                {
                    SpawnCustomer();
                }

                foreach (Customer c in customers)
                {
                    c.Move(orderPlace, this);
                } */
    }

    public void ReachedTarget(Customer customer)
    {
        firstCustomer = customer;
        foreach (Customer c in customers)
        {
            c.Move(null, this); //this stops all the customers
        }

       // if (customers.Count < maxCustomerN)
        if (GameController.curWaveInd >= gameController.orderWaves.Count)   //if its the last wave, we should loop the customers
        {
             SpawnCustomer();
            /*             GameObject newCustomerGO = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], customerSpawnPlace.position, customers[0].transform.rotation);
                        newCustomerGO.transform.SetParent(transform);

                        Customer c = newCustomerGO.GetComponent<Customer>();

                        SubscribeCustomer(c);

                        c.FadeIn(1f); */
        }
    }

    public void SpawnCustomer()
    {
        if (lastCustomer == null)
        {
            GameObject newCustomerGO = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], orderPlace.position, Quaternion.identity);
            newCustomerGO.transform.SetParent(transform);
            newCustomerGO.transform.forward = -Vector3.forward;

            Customer c = newCustomerGO.GetComponent<Customer>();

            SubscribeCustomer(c);

            c.FadeIn(1f);

            lastCustomer = c;
        }
        else
        {
            GameObject newCustomerGO = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], new Vector3(lastCustomer.transform.position.x, lastCustomer.transform.position.y, lastCustomer.transform.position.z + 0.483757f), Quaternion.identity);
            newCustomerGO.transform.SetParent(transform);
            newCustomerGO.transform.forward = -Vector3.forward;

            Customer c = newCustomerGO.GetComponent<Customer>();

            SubscribeCustomer(c);

            c.FadeIn(1f);

            lastCustomer = c;
            // 0.483757f
        }

    }

    public void SpawnNewCustomerWave()
    {
        int customerN = 0;
        if (GameController.curWaveInd < gameController.orderWaves.Count)
        {
            customerN = gameController.orderWaves[GameController.curWaveInd].orders.Count;
        }
        else //if its the last wave, set customerN to 5 and the customer will loop in ReachedTarget(Customer customer)
        {
            customerN = 5;
        }

        for (int i = 0; i < customerN; i++)
        {
            SpawnCustomer();
        }

        foreach (Customer c in customers)
        {
            c.Move(orderPlace, this);
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
        if (firstCustomer == lastCustomer) lastCustomer = null;

        Destroy(firstCustomer.gameObject, fadeOutStartDelay + fadeOutDuration + 0.5f);
        //Destroy(firstCustomer.gameObject);//change this to customer leaving and then destroying
        firstCustomer = null;

        foreach (Customer c in customers)
        {
            c.Move(orderPlace, this);
        }    //here somewhere spawn another customer
    }
}
