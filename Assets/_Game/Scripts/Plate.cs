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
    public Transform placeForFoodModel = null;
    public Transform transformValuesWhenHeld = null;
    public PlateScriptableObject plateScriptableObject = null;
    public ProgressCircle progressCircle = null;
    public Transform placesForIngredientsParent = null;

    public List<PlateFood> plateFoods = null;

    public GameObject plateModel = null;



    private PlayerController playerController = null;

    private List<PreparedIngredient> ingredients = new List<PreparedIngredient>();
    private int ingrPlaceInd = 0;

    private GameObject newPlate = null;

    private bool isCombining = false;
    private float combiningTimer=0f, timeToCombine = 0.5f;

    private GameObject foundFoodModelPrefab = null;

    public List<PreparedIngredient> Ingredients { get => ingredients; set => ingredients = value; }

    private void Update()
    {
        if (isCombining)
        {
            if (combiningTimer == 0f)
            {
                progressCircle.ShowCircle(true);
            }
            combiningTimer += Time.deltaTime;
            if (combiningTimer > timeToCombine)
            {
                combiningTimer = timeToCombine;
                SetPlateFoodModel();
                GetComponent<Collider>().enabled = false;
                isCombining = false;
                Invoke(nameof(HideProgressCircleAfterDelay), 0.25f);
                //scale out ingredients
                //scale in platefood
                //after scale in, allow plate pickup
                //  Invoke(nameof(DisappearAfterDelay), 0.33f);
                //  isBought = true;
            }
            progressCircle.SetProgress(combiningTimer / timeToCombine);
        }
    }

    public void HideProgressCircleAfterDelay()
    {
        progressCircle.ShowCircle(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        { //if player collides
            playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.PlayerState == PlayerStates.Holding)
            {   //if plate not full would be a better question for this if statement
                if (ingrPlaceInd < placesForIngredients.Count)
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

                    foundFoodModelPrefab = FindPlateFood(ingredients);
                    if (foundFoodModelPrefab == null)
                    {
                        TakePlate();
                    }
                    else
                    {
                        isCombining = true;
                    }
                }
            }
        }
    }

    private void TakePlate()
    {
        newPlate = Instantiate(plateScriptableObject.platePrefab, transform.position, Quaternion.identity);
        newPlate.transform.SetParent(transform.parent);
        newPlate.SetActive(false);

        Invoke(nameof(ShowNewPlateAfterDelay), 0.5f);
        //   SetPlateFoodModel(foundFoodModel);

        playerController.SetHoldableObject(this);

        transform.localPosition = transformValuesWhenHeld.localPosition;
        transform.localRotation = transformValuesWhenHeld.localRotation;

        //transform.up=Vector3.up;
        GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        isCombining = false;
        combiningTimer = 0f;
        progressCircle.SetProgress(0f);
        progressCircle.ShowCircle(false);
    }

    private void SetPlateFoodModel()
    {
        if (foundFoodModelPrefab == null) return;
        GameObject foodModelGO = Instantiate(foundFoodModelPrefab);
        foodModelGO.transform.position = placeForFoodModel.position;
        foodModelGO.transform.SetParent(placeForFoodModel);

        Food foodScript = foodModelGO.GetComponent<Food>();
        if (foodScript != null)
        {
            if (foodScript.hasPlate)
            {
                plateModel.SetActive(false);
            }
        }

        Vector3 foodModelDefaultScale = foodModelGO.transform.localScale;
        foodModelGO.transform.localScale = Vector3.zero;

        Sequence tweenSeq = DOTween.Sequence();
        tweenSeq.Append(placesForIngredientsParent.transform.DOScale(Vector3.zero, 0.1f));
        tweenSeq.Append(foodModelGO.transform.DOScale(foodModelDefaultScale, 0.1f));


        tweenSeq.AppendCallback(() =>
        {
            Destroy(placesForIngredientsParent.gameObject);
            TakePlate();
        });

    }

    private GameObject FindPlateFood(List<PreparedIngredient> prepIngrList)
    {
        IngredientType[] ingredientTypes = new IngredientType[prepIngrList.Count];
        for (int i = 0; i < ingredientTypes.Length; i++)
        {
            ingredientTypes[i] = prepIngrList[i].ingrType;
        }
        return FindPlateFood(ingredientTypes);
    }

    private GameObject FindPlateFood(IngredientType[] ingrList)
    {
        bool isMatching = false;
        for (int i = 0; i < plateFoods.Count; i++)
        {
            if (plateFoods[i].ingredients.Count == ingrList.Length)
            {
                for (int j = 0; j < plateFoods[i].ingredients.Count; j++)
                {
                    for (int k = 0; k < ingrList.Length; k++)
                    {
                        if (plateFoods[i].ingredients[j] == ingrList[k])
                        {
                            isMatching = true;
                            break;
                        }
                        if ((k + 1) == ingrList.Length) isMatching = false;
                    }
                    if (!isMatching) break;

                    if ((j + 1) == plateFoods[i].ingredients.Count) //here all the ingredients match
                    {
                        return plateFoods[i].foodPrefab;
                    }
                }
            }
        }
        return null;
    }
    private void ShowNewPlateAfterDelay()
    {
        Vector3 plateDefaultScale = newPlate.transform.localScale;
        newPlate.transform.localScale = Vector3.zero;
        newPlate.SetActive(true);
        newPlate.transform.DOScale(plateDefaultScale, 0.33f);
    }
}
