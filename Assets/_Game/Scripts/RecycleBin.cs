using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;
using DG.Tweening;

public class RecycleBin : MonoBehaviour
{
    public GameObject recycleBinModel=null;
    private PlayerController playerController = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HeldObject == null) return;

            playerController.SetHoldableObject(null, true);
            recycleBinModel.transform.DOPunchScale(recycleBinModel.transform.localScale*0.2f,0.2f,10,0.5f);

            playerController.SuccesfulTrigger(transform);
        }
    }
}
