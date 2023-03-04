using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerNamespace;

public class WalkingSpeedUpgradeButton : UpgradeButton
{


    private void Start()
    {
        price = pricesForLevels[0];
        moneyText.text = price + "";

        maxLevel = pricesForLevels.Count;
    }

    protected override void UpgradeEffect()
    {
        PlayerController.movementSpeed = upgradeValuesForLevels[level];
    }
}
