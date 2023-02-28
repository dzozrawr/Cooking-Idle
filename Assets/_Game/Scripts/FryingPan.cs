using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;
using HoldableNameSpace;

public class FryingPan : MonoBehaviour
{
    public Transform placeForIngredient = null;

    public ProgressCircle progressCircle = null;


    private PlayerController playerController = null;

    private HoldableObject ingredient = null, cookedIngredient = null;

    private PanFryableIngredient panFryableIngredient = null;

    private float timeToChop = 3f;

    private float choppingTimer = 0f;

    private void Update()
    {
        if (ingredient != null)
        {
            if (choppingTimer == 0f) {
                progressCircle.ShowCircle(true);
                //   knifeChopper.ToggleChoppingPlay(true);
            }
            choppingTimer += Time.deltaTime;

            progressCircle.SetProgress(choppingTimer / timeToChop);
            panFryableIngredient.CookingEffect(choppingTimer / timeToChop);
            //here also update the progress bar
            if (choppingTimer > timeToChop)
            {
                choppingTimer = 0f;
                progressCircle.SetProgress(1f);
                cookedIngredient = Instantiate(ingredient.GetComponent<FreshIngredient>().preparedIngred.gameObject).GetComponent<HoldableObject>();
                cookedIngredient.transform.position = placeForIngredient.position;
                cookedIngredient.transform.SetParent(placeForIngredient);

                Destroy(ingredient.gameObject);
                ingredient = null;

                Invoke(nameof(HideProgressCircleAfterDelay), 0.25f);

                //   knifeChopper.ToggleChoppingPlay(false);
            }
        }
    }

    private void CookingEffect(float progress)
    {

    }

    public void HideProgressCircleAfterDelay()
    {
        progressCircle.ShowCircle(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ingredient != null) return; //ingredient is in the process of cutting, return
        if (other.gameObject.tag.Equals("Player"))
        {//if player collides
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (cookedIngredient == null)  //if the board is empty
            {
                if (playerController.PlayerState==PlayerStates.Holding)    //player is holding something so leave the held object on the board
                {
                    if (!(playerController.HeldObject is PanFryableIngredient)) return;  //the player is not holding a fresh ingredient, return
                    ingredient=playerController.HeldObject;
                    panFryableIngredient = (PanFryableIngredient)ingredient;

                    //place the ingredient that player is holding on the board
                    ingredient.transform.position = placeForIngredient.position;
                    ingredient.transform.SetParent(placeForIngredient);
                    ingredient.transform.rotation = Quaternion.identity;


                    playerController.SetHoldableObject(null);
                }
            }
            else //if the board holds a chopped ingredient    //here we pick up the chopped ingredient if possible
            {
                if (playerController.PlayerState==PlayerStates.Holding) return; 

                playerController.SetHoldableObject(cookedIngredient);

                cookedIngredient = null;
            }
        }
    }

}
