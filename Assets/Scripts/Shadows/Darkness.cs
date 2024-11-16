using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    SpriteRenderer darkness;
    public static Darkness settings;


    private void Awake()
    {
        settings = this;
    }

    public void SetDarkness(float alpha)
    {
        if (darkness == null)darkness = GetComponent<SpriteRenderer>();
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        darkness.GetPropertyBlock(mpb);
        mpb.SetColor("_Color", new Color(0, 0, 0, alpha));
        darkness.SetPropertyBlock(mpb);
    }

}
