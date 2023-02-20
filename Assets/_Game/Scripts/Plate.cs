using System.Collections;
using System.Collections.Generic;
using PlayerNamespace;
using UnityEngine;
using HoldableNameSpace;

public class Plate : HoldableObject
{
    public Transform placeForIngredient=null;
    public Transform transformValuesWhenHeld=null;
    private PlayerController playerController = null;

    private List<HoldableObject> ingredients=new List<HoldableObject>();
    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        { //if player collides
            playerController = other.gameObject.GetComponent<PlayerController>();
            if(ingredients.Count==0){   //if plate not full would be a better question for this if statement
                if(playerController.PlayerState==PlayerStates.Holding){ //maybe this is a redundant state
                    if(playerController.HeldObject.type==HoldableType.PreparedIngred){
                        HoldableObject preparedIngred=playerController.HeldObject;
                        ingredients.Add(playerController.HeldObject);

                        preparedIngred.transform.position = placeForIngredient.position;
                        preparedIngred.transform.SetParent(placeForIngredient);
                        preparedIngred.transform.rotation = Quaternion.identity;

                        playerController.SetHoldableObject(null);
                    }
                }
            }else{
                if(playerController.PlayerState==PlayerStates.Default){
                    //Quaternion oldRot= transform.rotation;
                    playerController.SetHoldableObject(this);

                    Invoke(nameof(RotateAfterDelay),0.1f);
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
