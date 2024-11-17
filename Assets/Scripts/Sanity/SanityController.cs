using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityController : MonoBehaviour
{    
    public float maxSanity = 100;
    public float currentSanity;

    public SanityBar sanityBar;

    void Start()
    {
        currentSanity = maxSanity;
        sanityBar.SetMaxSanity(maxSanity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            TakeSanityDamage(10);
        }
    }

    public void TakeSanityDamage(float damage)
    {
        currentSanity -= damage;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
    
    }

    public void AddSanity(float sanity)
    {
        currentSanity += sanity;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        sanityBar.SetSanity(currentSanity);
    }

    public void ReduceSanityOverTime(float damagePerSecond)
    {
        float damage = damagePerSecond;
        currentSanity -= damage;
        currentSanity = Mathf.Clamp(currentSanity, 0, maxSanity);
        sanityBar.SetSanity(currentSanity);
       
    }
}


