using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;

    public void SetMaxPower(float power)
    {
        slider.maxValue = power;
        slider.value = power;
        fillImage.fillAmount = 1f;
    }

    public void SetPower(float power)
    {
        slider.value = power;
        fillImage.fillAmount = power / slider.maxValue;
    }
}
