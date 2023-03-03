using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanUpgradeButton : UpgradeButton
{
    public List<float> cookingTimesForLevels = null;
    public List<int> pricesForLevels = null;

    private int level = 0;

    private int maxLevel = 10;



    private void Start()
    {
        price = pricesForLevels[0];
        moneyText.text = price + "";
    }
    protected override void OnClick()
    {



        if ((level + 1) < maxLevel)
        {
            if (price <= GameController.CoinAmount)
            {
                GameController.Instance.AddMoney(-price);
                FryingPan.timeToCook = cookingTimesForLevels[level];
                if ((level + 1) < pricesForLevels.Count)
                {
                    price = pricesForLevels[level + 1];
                    moneyText.text = price + "";
                }
                level++;

                if ((level + 1) >= maxLevel)
                {
                    button.enabled = false;
                    //also grey out the button
                }
            }
        }


    }
}
