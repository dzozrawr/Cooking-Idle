using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;
using HoldableNameSpace;

public class FryingPan : CookingTool
{

    public ParticleSystem cookingParticles = null;


    private PlayerController playerController = null;

    private PanFryableIngredient panFryableIngredient = null;

    private float timeToChop = 3f;

    private float choppingTimer = 0f;

    private void Update()
    {
        if (ingredient != null)
        {
            if (choppingTimer == 0f)
            {
                progressCircle.ShowCircle(true);
                cookingParticles.Play();
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
                preparedIngredient = Instantiate(ingredient.GetComponent<FreshIngredient>().preparedIngred.gameObject).GetComponent<HoldableObject>();
                preparedIngredient.transform.position = placeForIngredient.position;
                preparedIngredient.transform.SetParent(placeForIngredient);

                Destroy(ingredient.gameObject);
                ingredient = null;

                Invoke(nameof(HideProgressCircleAfterDelay), 0.25f);

                cookingParticles.Stop();
                //   knifeChopper.ToggleChoppingPlay(false);
            }
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
                    if ((!(playerController.HeldObject is PanFryableIngredient)) && (!(playerController.HeldObject is Egg))) return;  //the player is not holding a fresh ingredient, return

                    ingredient = playerController.HeldObject;

                    if (ingredient is Egg)
                    {
                        Egg egg = (Egg)ingredient;
                        GameObject sunnyEggGO = Instantiate(egg.sunnyEgg.gameObject);
                        ingredient = sunnyEggGO.GetComponent<PanFryableIngredient>();

                        playerController.SetHoldableObject(null, true);
                    }

                    panFryableIngredient = (PanFryableIngredient)ingredient;

                    //place the ingredient that player is holding on the board
                    ingredient.transform.position = placeForIngredient.position;
                    ingredient.transform.SetParent(placeForIngredient);
                    ingredient.transform.rotation = Quaternion.identity;

                    if (!(ingredient is Egg))
                        playerController.SetHoldableObject(null);
                    playerController.SuccesfulTrigger(transform);
                }
            }
            else //if the board holds a chopped ingredient    //here we pick up the chopped ingredient if possible
            {
                if (playerController.PlayerState == PlayerStates.Holding) return;

                playerController.SetHoldableObject(preparedIngredient);

               // cookingParticles.Stop();
                preparedIngredient = null;
                playerController.SuccesfulTrigger(transform);
            }
        }
    }

}
