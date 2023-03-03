using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class UpgradeButton : MonoBehaviour
{
    public TMP_Text moneyText = null;
    public Image moneyImg = null;
    public Color disabledButtonColor;

    public List<int> pricesForLevels = null;
    public List<float> upgradeValuesForLevels = null;
    protected int price;

    protected int level = 0;

    protected int maxLevel = 10;

    protected Button button;
    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
        if (level < maxLevel)
        {
            if (price <= GameController.CoinAmount)
            {
                GameController.Instance.AddMoney(-price);
                UpgradeEffect();
                //FryingPan.timeToCook = upgradeValuesForLevels[level];
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
            }
        }
    }
    private void SetMaxLevelDisable()
    {
        moneyImg.enabled = false;
        moneyText.text = "MAX";
        button.GetComponent<Image>().color = disabledButtonColor;
    }
    protected abstract void UpgradeEffect();
}
