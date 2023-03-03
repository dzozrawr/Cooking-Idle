using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aezakmi;

public class GuidingIndicator : MonoBehaviour
{
    // public Canvas indicatorCanvas=null;
    // public Transform indicatorToControl=null;

    public ArrowIndicator arrowIndicator = null;

    public delegate void GuidingIndicatorEvent();
    public GuidingIndicatorEvent TargetReached;

    private bool isEnabled = false;

    private Transform target = null;

    public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

    private void Awake()
    {
        arrowIndicator.Renderer.enabled = false;
    }

    private void Update()
    {
/*         if (isEnabled)
        {
            if (Vector3.Distance(transform.position, target.position) < 1.5f)
            {
                SetEnabled(false);
                TargetReached?.Invoke();
            }
        } */
    }

    public void SetEnabled(bool shouldEnable)
    {
        //arrowIndicator
        arrowIndicator.Renderer.enabled = shouldEnable;
        // indicatorCanvas.enabled=shouldEnable;        
        this.isEnabled = shouldEnable;
    }

    public void SetTargetAndEnable(Transform t)
    {
        target = t;
        arrowIndicator.Target = t;
        SetEnabled(true);
    }

    public void PlayerTriggered(Transform triggerTarget)
    {
        if (isEnabled)
        {
            if (triggerTarget == target)
            {
                SetEnabled(false);
                TargetReached?.Invoke();
            }
        }

    }
}
