using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi;

public class GuidingIndicator : MonoBehaviour
{
   // public Canvas indicatorCanvas=null;
   // public Transform indicatorToControl=null;

    public ArrowIndicator arrowIndicator=null;

    private bool isEnabled=false;

    private Transform target=null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetEnabled(bool shouldEnable){
        //arrowIndicator
        arrowIndicator.Renderer.enabled=shouldEnable;
       // indicatorCanvas.enabled=shouldEnable;        
        this.isEnabled=shouldEnable;
    }

    public void SetTargetAndEnable(Transform t){
        arrowIndicator.Target=t;
        SetEnabled(true);
    }
}
