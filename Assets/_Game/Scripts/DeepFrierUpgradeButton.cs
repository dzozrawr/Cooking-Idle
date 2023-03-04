using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeepFrierUpgradeButton : UpgradeButton
{

    public static int levelOverride = 0;


    private void Start()
    {

        maxLevel = pricesForLevels.Count;

        if (levelOverride == 0)
        {
            price = pricesForLevels[0];
            moneyText.text = price + "";
        }
        if (levelOverride > 0)
        {
            level = levelOverride - 1;    //take a step back to apply the upgrade properly
            SetLevel(level);
        }
    }



    protected override void UpgradeEffect()
    {
        DeepFrier.timeToCook = upgradeValuesForLevels[level];
    }

    protected override void LevelUpdate()
    {
        levelOverride = level;
    }
}
