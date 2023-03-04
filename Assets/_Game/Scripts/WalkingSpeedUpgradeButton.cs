using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerNamespace;

public class WalkingSpeedUpgradeButton : UpgradeButton
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

/*    private void SetLevel(int level)
    {
        if (level == 0) return;

        UpgradeEffect();
        if ((level + 1) < pricesForLevels.Count)
        {
            price = pricesForLevels[level + 1];
            moneyText.text = price + "";
        }
        level++;

        if (level >= maxLevel)
        {
            SetMaxLevelDisable();
            //also grey out the button
        }
    }*/
    protected override void UpgradeEffect()
    {
        PlayerController.movementSpeed = upgradeValuesForLevels[level];
    }

    protected override void LevelUpdate()
    {
        levelOverride = level;
    }
}
