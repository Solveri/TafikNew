using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;

    public void SetBar(float minvalue ,float maxValue,float value)
    {
        slider.minValue = minvalue;
       slider.maxValue = maxValue;
        slider.value = value;
    }
    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
