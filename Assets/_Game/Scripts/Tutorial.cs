using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;
using Ingredients;

public class Tutorial : MonoBehaviour
{
    private enum IngredientPhase
    {
        PickUp, PickUp2, PickUp3, Preparing, Preparing2, ToPlate, Finished
    }
    private class IngrTutorial
    {
        public IngredientType type;
        public IngredientPhase phase;
        public Transform prevTarget = null;
    }

    public List<Pot> pots = null;
    public List<FryingPan> pans = null;
    public List<CuttingBoard> cuttingBoards = null;
    public List<DeepFrier> deepFriers = null;
    public Plate plate = null;
    public FinishSpot finishSpot = null;    //should be a list

    private GameController gameController = null;
    private PlayerController player = null;
    private IngredientBoxHolder ingredientBoxHolder = null;

    private List<IngredientType> ingredientList = null;

    private List<IngrTutorial> ingrTutorials = new List<IngrTutorial>();

    private int ingrN = 0;
    private int ingrIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        ingredientBoxHolder = IngredientBoxHolder.Instance;
        player = gameController.playerController;

        ingredientList = gameController.ActiveOrder.ingredientList;

        player.guidingIndicator.TargetReached += UpdateTutorial;

        //player.guidingIndicator.SetTargetAndEnable(ingredientBoxHolder.GetIngredientBox(ingredientList[0]).transform);

        foreach (IngredientType it in ingredientList)
        {
            IngrTutorial ingrTutorial = new IngrTutorial();
            ingrTutorial.type = it;
            ingrTutorials.Add(ingrTutorial);
            Debug.Log(it);
        }

        ingrN = ingrTutorials.Count;



        UpdateTutorialCore();
    }

    IEnumerator FrameEndWait()
    {
        yield return null;
        UpdateTutorialCore();
    }
    public void UpdateTutorial()
    {
        StartCoroutine(FrameEndWait());
    }

    private void UpdateTutorialCore()
    {
        // Debug.Log("ingrTutorials[ingrIndex].phase == IngredientPhase.PickUp");
        if (ingrTutorials[ingrIndex].phase == IngredientPhase.PickUp)
        {
            Transform target = ingredientBoxHolder.GetIngredientBox(ingrTutorials[ingrIndex].type).transform;
            player.guidingIndicator.SetTargetAndEnable(target);
            ingrTutorials[ingrIndex].phase = IngredientPhase.PickUp2;

            ingrTutorials[ingrIndex].prevTarget = target;
            return;
        }

        if (ingrTutorials[ingrIndex].phase == IngredientPhase.PickUp2)
        {
            if (player.HeldObject is PotCookableIngridient)
            {
                foreach (Pot pot in pots)
                {
                    if (!pot.gameObject.activeSelf) continue;
                    if ((pot.Ingredient == null) && (pot.CookedIngredient == null))
                    {
                        player.guidingIndicator.SetTargetAndEnable(pot.transform);
                        ingrTutorials[ingrIndex].prevTarget = pot.transform;
                        break;
                    }
                }
                //find a free pot
            }
            else
            if (player.HeldObject is PanFryableIngredient)
            {

            }
            else
            if (player.HeldObject is ChoppableIngredient)
            {

                foreach (CuttingBoard cuttingBoard in cuttingBoards)
                {
                    if (!cuttingBoard.gameObject.activeSelf) continue;
                    if ((cuttingBoard.Ingredient == null) && (cuttingBoard.ChoppedIngredient == null))
                    {
                        player.guidingIndicator.SetTargetAndEnable(cuttingBoard.transform);
                        ingrTutorials[ingrIndex].prevTarget = cuttingBoard.transform;
                        break;
                    }
                }
            }
            else
            if (player.HeldObject is DeepFryableIngridient)
            {

            }

            ingrTutorials[ingrIndex].phase = IngredientPhase.Preparing;
            ingrIndex = (ingrIndex + 1) % ingrN;


            return;
        }


        if (ingrTutorials[ingrIndex].phase == IngredientPhase.Preparing)
        {
            player.guidingIndicator.SetTargetAndEnable(ingrTutorials[ingrIndex].prevTarget);
            ingrTutorials[ingrIndex].phase = IngredientPhase.Preparing2;
            //GetComponent cooking device, get ingredient
            return;
        }

        if (ingrTutorials[ingrIndex].phase == IngredientPhase.Preparing2)
        {
            if(player.HeldObject is PreparedIngredient)
            {
                player.guidingIndicator.SetTargetAndEnable(plate.transform);
                ingrTutorials[ingrIndex].prevTarget=plate.transform;

                ingrTutorials[ingrIndex].phase = IngredientPhase.ToPlate;
                ingrIndex = (ingrIndex + 1) % ingrN;
            }
            else
            {
                ingrTutorials[ingrIndex].phase = IngredientPhase.PickUp2;
                UpdateTutorialCore();
            }

            return;
        }

        if(ingrTutorials[ingrIndex].phase == IngredientPhase.Finished)
        {
            player.guidingIndicator.SetTargetAndEnable(finishSpot.transform);
            return;
        }

        bool areAllToPlate = true;
        foreach (IngrTutorial iT in ingrTutorials)
        {
            if (iT.phase != IngredientPhase.ToPlate)
            {
                areAllToPlate = false;
            }
        }
        if (areAllToPlate)
        {
            player.guidingIndicator.SetTargetAndEnable(plate.transform);
            foreach (IngrTutorial iT in ingrTutorials)
            {
                iT.phase = IngredientPhase.Finished;
            }
            return;
        }

    }




}
