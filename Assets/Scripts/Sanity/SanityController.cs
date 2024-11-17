using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanityController : MonoBehaviour
{
    public int maxSanity = 100;
    public int currentSanity;

    public SanityBar sanityBar;

    private void Start()
    {
        currentSanity = maxSanity;
        sanityBar.SetMaxSanity(maxSanity);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeSanityDamage(10);
        }
    }

    public void TakeSanityDamage(int damage)
    {
        currentSanity -= damage;
        sanityBar.SetSanity(currentSanity);

        if (currentSanity < 0)
        {
            currentSanity = 0;
        }
    }

    public void AddSanity(int sanity)
    {
        currentSanity += sanity;
        sanityBar.SetSanity(currentSanity);

        if (currentSanity > maxSanity)
        {
            currentSanity = maxSanity;
        }
    }
}


