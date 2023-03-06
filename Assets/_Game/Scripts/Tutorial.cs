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

    public List<CookingTool> pots = null;
    public List<CookingTool> pans = null;
    public List<CookingTool> cuttingBoards = null;
    public List<CookingTool> deepFriers = null;
    public Plate plate = null;
    public FinishSpot finishSpot = null;    //should be a list

    public RecycleBin recycleBin = null;

    public TriggerSpotUpgrade upgradeSpot = null;

    private GameController gameController = null;
    private PlayerController player = null;
    private IngredientBoxHolder ingredientBoxHolder = null;

    private List<IngredientType> ingredientList = null;

    private List<IngrTutorial> ingrTutorials = new List<IngrTutorial>();

    private int ingrN = 0;
    private int ingrIndex = 0;

    private bool toFinishSpot = false, isOrderDone = false;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        ingredientBoxHolder = IngredientBoxHolder.Instance;
        player = gameController.playerController;



        player.guidingIndicator.TargetReached += UpdateTutorial;
        gameController.NewOrderAppeared += InitTutorial;
        gameController.UpgradeSpotTriggered += UgradeSpotTriggered;

        //player.guidingIndicator.SetTargetAndEnable(ingredientBoxHolder.GetIngredientBox(ingredientList[0]).transform);


        InitTutorial();


        //        UpdateTutorialCore();
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

    private void UgradeSpotTriggered()
    {
        upgradeSpot = null;
    }

    private void UpdateTutorialCore()
    {
       // Debug.Log("UpdateTutorialCore()");
        if (GameController.curWaveInd == 1 && upgradeSpot != null)
        {
          //  Debug.Log("GameController.curWaveInd == 1 && upgradeSpot != null");
            player.guidingIndicator.SetTargetAndEnable(upgradeSpot.transform);
            
            return;
        }

        // Debug.Log("ingrIndex="+ingrIndex);
        if (isOrderDone)
        {
            player.guidingIndicator.SetEnabled(false);
            isOrderDone = false;

           // ResetTutorial();
            return;
        }
        if (toFinishSpot)
        {
            player.guidingIndicator.SetTargetAndEnable(finishSpot.transform);
            toFinishSpot = false;
            isOrderDone = true;
            return;
        }
        // Debug.Log("ingrTutorials[ingrIndex].phase == IngredientPhase.PickUp");
        if (ingrTutorials[ingrIndex].phase == IngredientPhase.PickUp)
        {
            Transform target = ingredientBoxHolder.GetIngredientBox(ingrTutorials[ingrIndex].type).transform;
            player.guidingIndicator.SetTargetAndEnable(target);
            ingrTutorials[ingrIndex].phase = IngredientPhase.PickUp2;

            ingrTutorials[ingrIndex].prevTarget = target;

            //  Debug.Log(ingrTutorials[ingrIndex].type);
            return;
        }

        if (ingrTutorials[ingrIndex].phase == IngredientPhase.PickUp2)
        {
            if (player.HeldObject is PreparedIngredient)
            {
                player.guidingIndicator.SetTargetAndEnable(plate.transform);
                ingrTutorials[ingrIndex].prevTarget = plate.transform;

                ingrTutorials[ingrIndex].phase = IngredientPhase.ToPlate;
                ingrIndex = (ingrIndex + 1) % ingrN;
                return;
            }
            else
            if (player.HeldObject is PotCookableIngridient)
            {
                TargetFreeCookingTool(pots);
                //find a free pot
            }
            else
            if (player.HeldObject is PanFryableIngredient)
            {
                TargetFreeCookingTool(pans);
            }
            else
            if (player.HeldObject is ChoppableIngredient)
            {
                TargetFreeCookingTool(cuttingBoards);
            }
            else
            if (player.HeldObject is DeepFryableIngridient)
            {
                TargetFreeCookingTool(deepFriers);
            }

            if (!player.guidingIndicator.IsEnabled)
            { //no available cooking tools, so go to trash can
                player.guidingIndicator.SetTargetAndEnable(recycleBin.transform);
                ingrTutorials[ingrIndex].phase = IngredientPhase.PickUp;
                ingrIndex = (ingrIndex + 1) % ingrN;
                return;
                //go to trash can
            }

            ingrTutorials[ingrIndex].phase = IngredientPhase.Preparing;
            //   Debug.Log("ingrTutorials[ingrIndex].phase = IngredientPhase.Preparing;");
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
            if (player.HeldObject is PreparedIngredient)
            {
                player.guidingIndicator.SetTargetAndEnable(plate.transform);
                ingrTutorials[ingrIndex].prevTarget = plate.transform;

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



        bool areAllToPlate = true;

        for (int i = 0; i < ingrTutorials.Count; i++)
        {
            if (ingrTutorials[i].phase != IngredientPhase.ToPlate)
            {
                areAllToPlate = false;
                ingrIndex = i;
                UpdateTutorialCore();
                return;
            }
        }
        if (areAllToPlate)
        {
            player.guidingIndicator.SetTargetAndEnable(plate.transform);
            toFinishSpot = true;
            return;
        }

    }

    private void ResetTutorial()
    {
        ingrTutorials = new List<IngrTutorial>();
        ingrN = 0;
        ingrIndex = 0;
    }
    private void InitTutorial()
    {
        ResetTutorial();
        if (plate == null)
        {
            plate = GameObject.FindObjectOfType<Plate>();
        }

        int activeOrderInd = 0;
        if (gameController.ActiveOrders[activeOrderInd] == null) activeOrderInd = 1;
        ingredientList = gameController.ActiveOrders[activeOrderInd].ingredientList;
        foreach (IngredientType it in ingredientList)
        {
            IngrTutorial ingrTutorial = new IngrTutorial();
            ingrTutorial.type = it;
            ingrTutorials.Add(ingrTutorial);
            ingrTutorial.phase = IngredientPhase.PickUp;

            //Debug.Log(it);
        }

        ingrN = ingrTutorials.Count;


        //Debug.Log(ingrN);

        UpdateTutorialCore();
    }

    private void TargetFreeCookingTool(List<CookingTool> cookingTools)
    {

        foreach (CookingTool cookingTool in cookingTools)
        {
            if (!cookingTool.gameObject.activeSelf) continue;
            if ((cookingTool.Ingredient == null) && (cookingTool.PreparedIngredient == null))
            {
                player.guidingIndicator.SetTargetAndEnable(cookingTool.transform);
                ingrTutorials[ingrIndex].prevTarget = cookingTool.transform;
                break;
            }
        }
    }




}
