using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    public Transform placeForIngredient = null;

    public ProgressCircle progressCircle = null;

    private PlayerController playerController = null;

    private GameObject ingredient = null, choppedIngredient = null;

    private float timeToChop = 3f;

    private float choppingTimer = 0f;

    private void Update()
    {
        if (ingredient != null)
        {
            if (choppingTimer == 0f) progressCircle.ShowCircle(true);
            choppingTimer += Time.deltaTime;

            progressCircle.SetProgress(choppingTimer / timeToChop);
            //here also update the progress bar
            if (choppingTimer > timeToChop)
            {
                choppingTimer=0f;
                progressCircle.SetProgress(1f);
                choppedIngredient = Instantiate(ingredient.GetComponent<IngredientScript>().choppedModel);
                choppedIngredient.transform.position = placeForIngredient.position;
                choppedIngredient.transform.SetParent(placeForIngredient);

                Destroy(ingredient);
                ingredient = null;

                Invoke(nameof(HideProgressCircleAfterDelay), 0.25f);

                
            }
        }
    }

    public void HideProgressCircleAfterDelay()
    {
        progressCircle.ShowCircle(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ingredient != null) return;
        if (other.gameObject.tag.Equals("Player"))
        {//if player collides
            playerController = other.gameObject.GetComponent<PlayerController>();
            Transform ingredientTransform=null;
            if(playerController.placeForIngredient.transform.childCount==0) ingredientTransform=null; else{
                ingredientTransform = playerController.placeForIngredient.transform.GetChild(0);
            }
            
            if (choppedIngredient == null)
            {                
                if ((ingredientTransform!=null)&&(ingredientTransform != null))    //player is holding something
                {
                    if(ingredientTransform.GetComponent<IngredientScript>()==null) return;  //the player is not holding a fresh ingredient, return
                    ingredient = playerController.placeForIngredient.transform.GetChild(0).gameObject;

                    //place the ingredient that player is holding on the board
                    ingredient.transform.position = placeForIngredient.position;
                    ingredient.transform.SetParent(placeForIngredient);
                    ingredient.transform.rotation = Quaternion.identity;


                    playerController.TriggerIdleAnim(); //trigger players idle animation

                    playerController.HeldObject=null;
                }
            }
            else
            {
                if((ingredientTransform!=null)&&(ingredientTransform.GetComponent<IngredientScript>()!=null)) return;  //the player is holding a chopped ingredient already, return
                choppedIngredient.transform.position = playerController.placeForIngredient.transform.position;
                choppedIngredient.transform.SetParent(playerController.placeForIngredient.transform);
                choppedIngredient.transform.rotation = Quaternion.identity;

                playerController.TriggerHoldingAnim();

                playerController.HeldObject=choppedIngredient;

                choppedIngredient=null;

               // GameObject newTomato = Instantiate(tomatoPrefab, playerController.placeForIngredient.transform.position, Quaternion.identity);//place the tomato in his hands in a predetermined place
              //  newTomato.transform.SetParent(playerController.placeForIngredient.transform);
            }
        }
    }
}
