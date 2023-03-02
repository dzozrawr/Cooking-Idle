using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerNamespace;
using Ingredients;

public class Tutorial : MonoBehaviour
{
    private GameController gameController = null;
    private PlayerController player = null;
    private IngredientBoxHolder ingredientBoxHolder = null;

    private List<IngredientType> ingredientList = null;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.Instance;
        ingredientBoxHolder = IngredientBoxHolder.Instance;
        player = gameController.playerController;

        ingredientList = gameController.ActiveOrder.ingredientList;

        player.guidingIndicator.TargetReached += UpdateTutorial;

        player.guidingIndicator.SetTargetAndEnable(ingredientBoxHolder.GetIngredientBox(ingredientList[0]).transform);
    }

    public void UpdateTutorial()
    {
        if (player.HeldObject is PotCookableIngridient)
        {

        }

        if (player.HeldObject is PanFryableIngredient)
        {

        }

        if (player.HeldObject is ChoppableIngredient)
        {

        }

        if (player.HeldObject is DeepFryableIngridient)
        {

        }
    }


}
