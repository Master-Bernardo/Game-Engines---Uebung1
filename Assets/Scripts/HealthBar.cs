using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarGauge;
    public float currentHealthRatio;

    private RectTransform rect;
    private float initGaugeWidth;


    // Start is called before the first frame update
    void Start()
    {
        rect = (RectTransform) healthBarGauge.transform;
        initGaugeWidth = rect.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        rect.sizeDelta = new Vector2(initGaugeWidth * currentHealthRatio, rect.sizeDelta.y);
    }

    public void SetCurrentHealthRatio(float ratio)
    {
        currentHealthRatio = ratio;
    }
}
