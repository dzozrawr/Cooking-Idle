using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    private PlayerController playerController = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HeldObject == null) return;

            playerController.SetHoldableObject(null);
        }
    }
}
