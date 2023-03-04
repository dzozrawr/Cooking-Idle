using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;

[DataContract]
public class SaveData
{
    [DataMember]
    public int money;

    [DataMember]
    public int panUpgradeLevel;

    [DataMember]
    public int potUpgradeLevel;

    [DataMember]
    public int cuttingBoardUpgradeLevel;

    [DataMember]
    public int deepFrierUpgradeLevel;

    [DataMember]
    public int walkingUpgradeLevel;

    public SaveData(int _level)
    {
       // level = _level;
        money = GameController.CoinAmount;   //implicit saving of the coin amount for simplicity of the constructor
        panUpgradeLevel = PanUpgradeButton.levelOverride;
        potUpgradeLevel = PotUgradeButton.levelOverride;
        cuttingBoardUpgradeLevel = CuttingBoardUpgradeButton.levelOverride;
        deepFrierUpgradeLevel = DeepFrierUpgradeButton.levelOverride;
        walkingUpgradeLevel = WalkingSpeedUpgradeButton.levelOverride;
    }

    public SaveData()
    {
       // level = SceneManager.GetActiveScene().buildIndex;
        money = GameController.CoinAmount;   //implicit saving of the coin amount for simplicity of the constructor
        panUpgradeLevel = PanUpgradeButton.levelOverride;
        potUpgradeLevel = PotUgradeButton.levelOverride;
        cuttingBoardUpgradeLevel = CuttingBoardUpgradeButton.levelOverride;
        deepFrierUpgradeLevel = DeepFrierUpgradeButton.levelOverride;
        walkingUpgradeLevel = WalkingSpeedUpgradeButton.levelOverride;
    }

}
