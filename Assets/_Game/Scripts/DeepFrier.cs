using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;
using HoldableNameSpace;

public class DeepFrier : CookingTool
{
    public static float timeToCook = 10f;
    public ParticleSystem cookingParticles = null;
    public AnimatedTextureUVs waterAnimation = null;

    private PlayerController playerController = null;


    private DeepFryableIngridient deepFriableIngredient = null;

  //  private float timeToCook = 3f;

    private float choppingTimer = 0f;

    private void Update()
    {
        if (ingredient != null)
        {
            if (choppingTimer == 0f)
            {
                progressCircle.ShowCircle(true);
                ToggleCookingFX(true);
                //   knifeChopper.ToggleChoppingPlay(true);
            }
            choppingTimer += Time.deltaTime;

            progressCircle.SetProgress(choppingTimer / timeToCook);
            // potCookableIngredient.CookingEffect(choppingTimer / timeToChop);
            //here also update the progress bar
            if (choppingTimer > timeToCook)
            {
                choppingTimer = 0f;
                progressCircle.SetProgress(1f);
                preparedIngredient = Instantiate(ingredient.GetComponent<FreshIngredient>().preparedIngred.gameObject).GetComponent<HoldableObject>();
                preparedIngredient.transform.position = placeForIngredient.position;
                preparedIngredient.transform.SetParent(placeForIngredient);

                Destroy(ingredient.gameObject);
                ingredient = null;

                Invoke(nameof(HideProgressCircleAfterDelay), 0.25f);
                ToggleCookingFX(false);
                //   knifeChopper.ToggleChoppingPlay(false);
            }
        }
    }
    public void ToggleCookingFX(bool shouldPlay)
    {
        if (shouldPlay)
        {
            cookingParticles.Play();
            waterAnimation.TogglePlay(shouldPlay);
        }
        else
        {
            cookingParticles.Stop();
            waterAnimation.TogglePlay(shouldPlay);
        }
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
            if (preparedIngredient == null)  //if the board is empty
            {
                if (playerController.PlayerState == PlayerStates.Holding)    //player is holding something so leave the held object on the board
                {
                    if (!(playerController.HeldObject is DeepFryableIngridient)) return;  //the player is not holding a fresh ingredient, return
                    ingredient = playerController.HeldObject;
                    deepFriableIngredient = (DeepFryableIngridient)ingredient;

                    //place the ingredient that player is holding on the board
                    ingredient.transform.position = placeForIngredient.position;
                    ingredient.transform.SetParent(placeForIngredient);
                    ingredient.transform.rotation = Quaternion.identity;
                    //ingredient.transform.rotation = Quaternion.Euler(deepFriableIngredient.rotationForDeepFryer) ;


                    playerController.SetHoldableObject(null);
                    playerController.SuccesfulTrigger(transform);
                }
            }
            else //if the board holds a chopped ingredient    //here we pick up the chopped ingredient if possible
            {
                if (playerController.PlayerState == PlayerStates.Holding) return;

                playerController.SetHoldableObject(preparedIngredient);

                //ToggleCookingFX(false);
                preparedIngredient = null;
                playerController.SuccesfulTrigger(transform);
            }
        }
    }

}
