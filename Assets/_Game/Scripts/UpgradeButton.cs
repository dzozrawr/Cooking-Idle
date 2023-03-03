using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeButton : MonoBehaviour
{
    public TMP_Text moneyText = null;
    protected int price;

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

    }
}
