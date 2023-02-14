using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressCircle : MonoBehaviour
{
    public Image img;
    public Canvas canvas=null;

    private float progress = 0f;
    private static float maxProgress = 1f;

    private float fillRate = 5f;

  //  public TMP_Text percentageText = null;
    // Start is called before the first frame update
    void Start()
    {
        img.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //img.fillAmount = Mathf.Lerp(img.fillAmount, progress, fillRate * Time.deltaTime);
        img.fillAmount=progress;
      //  percentageText.text = ((int)(img.fillAmount*100f)) + "%";
    }

    public void SetProgress(float p)
    {
        progress = p;
    }

    public void ShowCircle(bool shouldShow){
        canvas.enabled=shouldShow;
    }
}
