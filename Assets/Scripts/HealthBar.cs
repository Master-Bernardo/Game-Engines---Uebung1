using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarGauge;
    public float currentHealthRatio;

    private RectTransform rect;
    private Canvas canvas;
    private float initGaugeWidth;


    // Start is called before the first frame update
    void Start()
    {
        rect = (RectTransform) healthBarGauge.transform;
        canvas = GetComponent<Canvas>();
        initGaugeWidth = rect.sizeDelta.x;
        currentHealthRatio = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealthRatio >= 1)
        {
            if(canvas)
            {
                canvas.enabled = false;
            }
            rect.sizeDelta = new Vector2(initGaugeWidth * currentHealthRatio, rect.sizeDelta.y);
        }
        else
        {
            if(canvas)
            {
                canvas.enabled = true;
            }
            rect.sizeDelta = new Vector2(initGaugeWidth * currentHealthRatio, rect.sizeDelta.y);
        }
        
    }

    public void SetCurrentHealthRatio(float ratio)
    {
        currentHealthRatio = ratio;
    }
}
