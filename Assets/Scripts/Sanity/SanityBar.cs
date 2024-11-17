using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSanity(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetSanity(float health)
    {
        slider.value = health;
    }
}
