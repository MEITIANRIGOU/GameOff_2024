using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public float maxBatteryPower = 100f;
    public float drainRate = 5f;

    public float currentBatteryPower;

    SpriteRenderer flashlight;
    public PowerBar powerBar;

    SanityController sanityController;
    private bool isReducingSanity = false; 

    void Start()
    {
        currentBatteryPower = maxBatteryPower;
        flashlight = GameObject.FindWithTag("Flashlight").GetComponent<SpriteRenderer>();
        powerBar.SetMaxPower(maxBatteryPower);
        sanityController = GameObject.FindWithTag("Player").GetComponent<SanityController>();
    }

    void Update()
    {
        powerBar.SetPower(currentBatteryPower);

        if (currentBatteryPower > 0)
        {
            currentBatteryPower -= drainRate * Time.deltaTime;
            currentBatteryPower = Mathf.Max(currentBatteryPower, 0);

            isReducingSanity = false; 
        }

        if (currentBatteryPower <= 0 && !isReducingSanity)
        {
            FlashlightOff();
        }
    }

    void FlashlightOff()
    {
        if (flashlight != null)
        {
            flashlight.enabled = false;
        }

        if (!isReducingSanity)
        {
            isReducingSanity = true;
            StartCoroutine(ReduceSanityOverTime());
        }
    }

    private IEnumerator ReduceSanityOverTime()
    {
        while (true)
        {
            if (currentBatteryPower > 0)
            {
                yield break; 
            }

            sanityController.ReduceSanityOverTime(10f);
            yield return new WaitForSeconds(1f);
        }
    }


}
