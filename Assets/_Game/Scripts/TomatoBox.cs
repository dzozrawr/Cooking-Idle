using System.Collections;
using System.Collections.Generic;
using PlayerNamespace;
using UnityEngine;
using HoldableNameSpace;

public class TomatoBox : MonoBehaviour
{
    public HoldableObject tomatoPrefab = null;

    private PlayerController playerController = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HeldObject != null) return;

            GameObject newTomato = Instantiate(tomatoPrefab.gameObject);//place the tomato in his hands in a predetermined place
            playerController.SetHoldableObject(newTomato.GetComponent<HoldableObject>());
        }
    }
}
