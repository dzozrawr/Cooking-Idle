using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CameraUIBinder : MonoBehaviour
{
    public Canvas canvas=null;

    private Camera cameraUI=null;
    // Start is called before the first frame update
    void Start()
    {
        if(canvas==null){
            canvas=GetComponent<Canvas>();
        }

        if(canvas.worldCamera==null){
           canvas.worldCamera= GameObject.FindGameObjectWithTag("CameraUI").GetComponent<Camera>();
        }
    }

}
