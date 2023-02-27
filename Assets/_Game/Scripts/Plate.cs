using System.Collections;
using System.Collections.Generic;
using PlayerNamespace;
using UnityEngine;
using HoldableNameSpace;
using Ingredients;
using DG.Tweening;
using System;

public class Plate : HoldableObject
{
    [Serializable]
    public class PlateFood
    {
        public List<IngredientType> ingredients;
        public GameObject foodPrefab;
    }

    public List<Transform> placesForIngredients = null;
    public Transform transformValuesWhenHeld = null;
    public PlateScriptableObject plateScriptableObject = null;
    private PlayerController playerController = null;

    private List<PreparedIngredient> ingredients = new List<PreparedIngredient>();
    private int ingrPlaceInd = 0;

    private GameObject newPlate = null;

    public List<PreparedIngredient> Ingredients { get => ingredients; set => ingredients = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        { //if player collides
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (ingrPlaceInd < placesForIngredients.Count)
            {   //if plate not full would be a better question for this if statement
                if (playerController.PlayerState == PlayerStates.Holding)
                { //maybe this is a redundant state
                    if (playerController.HeldObject.type == HoldableType.PreparedIngred)
                    {
                        HoldableObject preparedIngred = playerController.HeldObject;
                        ingredients.Add((PreparedIngredient)playerController.HeldObject);

                        preparedIngred.transform.position = placesForIngredients[ingrPlaceInd].position;
                        preparedIngred.transform.SetParent(placesForIngredients[ingrPlaceInd]);
                        preparedIngred.transform.rotation = Quaternion.identity;

                        playerController.SetHoldableObject(null);
                        ingrPlaceInd++;
                    }
                }
            }
            else
            {
                if (playerController.PlayerState == PlayerStates.Default)
                {
                    //Quaternion oldRot= transform.rotation;
                    newPlate=Instantiate(plateScriptableObject.platePrefab, transform.position, Quaternion.identity);
                    newPlate.transform.SetParent(transform.parent);
                    newPlate.SetActive(false);
                    
                    Invoke(nameof(ShowNewPlateAfterDelay),0.5f);
                    playerController.SetHoldableObject(this);

                    transform.localPosition = transformValuesWhenHeld.localPosition;
                    transform.localRotation = transformValuesWhenHeld.localRotation;

                    //transform.up=Vector3.up;
                    GetComponent<Collider>().enabled = false;
                }
            }
        }
    }



    private void ShowNewPlateAfterDelay()
    {
        Vector3 plateDefaultScale=newPlate.transform.localScale;
        newPlate.transform.localScale=Vector3.zero;
        newPlate.SetActive(true);
        newPlate.transform.DOScale(plateDefaultScale,0.33f);
    }
}
