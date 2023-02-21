using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlateScriptableObject", order = 1)]
public class PlateScriptableObject : ScriptableObject
{
    public GameObject platePrefab;
}
