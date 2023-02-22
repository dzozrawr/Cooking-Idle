using System.Collections;
using System.Collections.Generic;
using PlayerNamespace;
using UnityEngine;
using HoldableNameSpace;
using Ingredients;

public class Plate : HoldableObject
{
    public List<Transform> placesForIngredients=null;
    public Transform transformValuesWhenHeld=null;
    public PlateScriptableObject plateScriptableObject = null;
    private PlayerController playerController = null;

    private List<PreparedIngredient> ingredients=new List<PreparedIngredient>();
    private int ingrPlaceInd = 0;

    private GameObject newPlate=null;

    public List<PreparedIngredient> Ingredients { get => ingredients; set => ingredients = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        { //if player collides
            playerController = other.gameObject.GetComponent<PlayerController>();
            if(ingrPlaceInd<placesForIngredients.Count)
            {   //if plate not full would be a better question for this if statement
                if(playerController.PlayerState==PlayerStates.Holding){ //maybe this is a redundant state
                    if(playerController.HeldObject.type==HoldableType.PreparedIngred){
                        HoldableObject preparedIngred=playerController.HeldObject;
                        ingredients.Add((PreparedIngredient)playerController.HeldObject);

                        preparedIngred.transform.position = placesForIngredients[ingrPlaceInd].position;
                        preparedIngred.transform.SetParent(placesForIngredients[ingrPlaceInd]);
                        preparedIngred.transform.rotation = Quaternion.identity;

                        playerController.SetHoldableObject(null);
                        ingrPlaceInd++;
                    }
                }
            }else{
                if(playerController.PlayerState==PlayerStates.Default){
                    //Quaternion oldRot= transform.rotation;

                    Instantiate(plateScriptableObject.platePrefab, transform.position,Quaternion.identity).transform.SetParent(transform.parent); 
                    playerController.SetHoldableObject(this);

                    transform.localPosition=transformValuesWhenHeld.localPosition;
                    transform.localRotation=transformValuesWhenHeld.localRotation;

                    //transform.up=Vector3.up;
                    GetComponent<Collider>().enabled=false;
                }
            }
        }
    }
    

    private void RotateAfterDelay(){
        //transform.Tra
        //transform.localRotation=Quaternion.Euler(0,0,0);
     //   transform.localRotation=Quaternion.Euler(-17.7f,26.9f,29.368f);
    }
}
