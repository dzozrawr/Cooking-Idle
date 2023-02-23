using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float speed = 2f;

    private Transform targetToMoveTowards = null;

    private Vector3 moveVector;

    private float curTargetDistance = 10f;
    private float prevTargetDistance = 10f;

    private CustomerManager customerManager = null;

    public Transform TargetToMoveTowards { get => targetToMoveTowards; set => targetToMoveTowards = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (targetToMoveTowards != null)
        {          
            curTargetDistance = Vector3.Distance(transform.position, targetToMoveTowards.position);
            if ((curTargetDistance < 0.01f) || (curTargetDistance > prevTargetDistance))   //if the spatula overstepped the target and started going away
            {
                targetToMoveTowards = null;
                customerManager.ReachedTarget();
               // transform.position = targetPos;
                return;
            }
            prevTargetDistance = curTargetDistance;

            transform.position = transform.position + moveVector * Time.deltaTime;
            //   Vector3 moveVector=
            //transform.position
        }
    }

    public void Move(Transform target,CustomerManager manager)
    {
        targetToMoveTowards = target;
        moveVector = target.position - transform.position;

        customerManager = manager;
    }
}
