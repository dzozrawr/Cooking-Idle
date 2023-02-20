using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoldableNameSpace
{
    public enum HoldableType{
        FreshIngred, PreparedIngred, Plate
    }
    public class HoldableObject : MonoBehaviour
    {
        public HoldableType type;
    }
}
