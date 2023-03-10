using System.Collections;
using System.Collections.Generic;
using PlayerNamespace;
using UnityEngine;
using HoldableNameSpace;
using DG.Tweening;
using Ingredients;

public class IngredientBox : MonoBehaviour
{
    public HoldableObject ingredientPrefab = null;
    public GameObject boxModel=null;
    public IngredientType ingredientType;

    private PlayerController playerController = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HeldObject != null) return;

            GameObject newTomato = Instantiate(ingredientPrefab.gameObject);//place the tomato in his hands in a predetermined place
            playerController.SetHoldableObject(newTomato.GetComponent<HoldableObject>());

            boxModel.transform.DOPunchScale(boxModel.transform.localScale*0.2f,0.2f,10,0.5f);

            playerController.SuccesfulTrigger(transform);
        }
    }
}
