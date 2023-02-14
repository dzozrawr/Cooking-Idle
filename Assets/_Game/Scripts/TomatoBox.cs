using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoBox : MonoBehaviour
{
    public GameObject tomatoPrefab = null;

    private PlayerController playerController = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HeldObject != null) return;

            GameObject newTomato = Instantiate(tomatoPrefab);//place the tomato in his hands in a predetermined place
            playerController.SetHoldableObject(newTomato);
        }
    }
}
