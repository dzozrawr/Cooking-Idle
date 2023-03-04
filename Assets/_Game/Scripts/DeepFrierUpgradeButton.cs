using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeepFrierUpgradeButton : UpgradeButton
{




    private void Start()
    {
        price = pricesForLevels[0];
        moneyText.text = price + "";

        maxLevel = pricesForLevels.Count;
    }



    protected override void UpgradeEffect()
    {
        DeepFrier.timeToCook = upgradeValuesForLevels[level];
    }
}
