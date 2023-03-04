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

    public SaveData(int _level)
    {
       // level = _level;
        money = GameController.CoinAmount;   //implicit saving of the coin amount for simplicity of the constructor
    }

    public SaveData()
    {
       // level = SceneManager.GetActiveScene().buildIndex;
        money = GameController.CoinAmount;   //implicit saving of the coin amount for simplicity of the constructor
    }

}
