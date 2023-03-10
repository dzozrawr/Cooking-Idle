using System.Collections;
using System.Collections.Generic;
//using Tabtale.TTPlugins;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private int levelToLoad = -1;
    private void Awake()
    {
        // TTPCore.Setup();
        levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;

        SaveData saveData = null;


        try
        {
            saveData = SaveSystem.LoadGameXML();
        }
        catch 
        {
            saveData = null;
        }

        if (saveData != null)
        {
            // levelToLoad = saveData.level;
            GameController.CoinAmount = saveData.money;
            PanUpgradeButton.levelOverride = saveData.panUpgradeLevel;
            PotUgradeButton.levelOverride = saveData.potUpgradeLevel;
            CuttingBoardUpgradeButton.levelOverride = saveData.cuttingBoardUpgradeLevel;
            DeepFrierUpgradeButton.levelOverride = saveData.deepFrierUpgradeLevel;
            WalkingSpeedUpgradeButton.levelOverride = saveData.walkingUpgradeLevel;
            GameController.curWaveInd = saveData.curOrderWaveInd;
            UnlockablesManager.unlockableStates = saveData.unlockablesStates;
            // ShopController.shopItemInfos=saveData.shopItemInfos;

            /*  WindmillScript.Lvl=saveData.windmillLevel;
             MarketScript.Lvl=saveData.marketLevel; */
        }
        else
        {
            PanUpgradeButton.levelOverride = 0;
            PotUgradeButton.levelOverride = 0;
            CuttingBoardUpgradeButton.levelOverride = 0;
            DeepFrierUpgradeButton.levelOverride = 0;
            WalkingSpeedUpgradeButton.levelOverride = 0;
            GameController.curWaveInd = 0;
            UnlockablesManager.unlockableStates = null;
            // GameController.missionID = 1;
        }
        SceneManager.LoadScene(levelToLoad);
    }

}
