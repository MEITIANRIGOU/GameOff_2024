using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ChestItems
{
    public GameObject item;
    [Range(0, 1)]
    public float probability;
}

public class Chest : MonoBehaviour
{
    [SerializeField] private KeyCode searchKey;
    [SerializeField] private float searchSpeed = 10.0f;
    [SerializeField] private float searchBoost = 0.5f;
    [SerializeField] private float searchDecreaseSpeed = 5.0f;
    [SerializeField] private float buttonHoldThreshold = 0.1f;
    [SerializeField] private Image radialLoaderUI;
    [SerializeField] List<ChestItems> chestItems;
    private float currentSearchProgress = 0.0f;
    private bool isPlayerInRange = false;
    private bool isSearchButtonPressed = false;
    private Coroutine searchCoroutine = null;
    private float searchButtonReleaseTime = 0.0f;
    private bool isSearchSuccessfull = false;
    private bool searchBoosted = false;
    private void Update()
    {
        if (!isPlayerInRange) return;

        HandleChestInputs();
    }

    private void HandleChestInputs()
    {
        if (Input.GetKeyDown(searchKey))
        {
            isSearchButtonPressed = true;

            if (searchCoroutine == null)
            {
                searchCoroutine = StartCoroutine(SearchCoroutine());
            }
        }

        if (Input.GetKeyUp(searchKey))
        {
            searchButtonReleaseTime = Time.time;
            isSearchButtonPressed = false;
            searchBoosted = false;
        }
    }

    private IEnumerator SearchCoroutine()
    {
        while (isSearchButtonPressed || Time.time - searchButtonReleaseTime <= buttonHoldThreshold && !isSearchSuccessfull)
        {
            currentSearchProgress += CalculateSearchProgress();
            radialLoaderUI.fillAmount = currentSearchProgress / 100.0f;
            if (currentSearchProgress >= 100)
            {
                OpenChest();

                yield break;
            }
            yield return null;
        }

        searchCoroutine = null;

        yield return new WaitForSeconds(0.2f);

        while (!isSearchSuccessfull && currentSearchProgress >= 0 && !isSearchButtonPressed)
        {
            currentSearchProgress -= searchBoost * Time.deltaTime * searchDecreaseSpeed;
            if (currentSearchProgress <= 0)
                currentSearchProgress = 0;
            radialLoaderUI.fillAmount = currentSearchProgress / 100.0f;
            yield return null;
        }
    }

    private float CalculateSearchProgress()
    {
        float progress = searchSpeed * Time.deltaTime;
        if (!searchBoosted && isSearchButtonPressed)
        {
            progress += searchBoost;
            searchBoosted = true;
            Debug.Log("Chest Smashed");
        }

        return progress;
    }

    private void OpenChest()
    {
        isSearchButtonPressed = false;
        isSearchSuccessfull = true;
        currentSearchProgress = 100.0f;
        //TODO: Remove this
        GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("Chest Opened");

        SpawChestItems();
    }

    private void SpawChestItems()
    {
        foreach (ChestItems item in chestItems)
        {
            float random = UnityEngine.Random.Range(0f, 1f);
            if (item.probability <= random)
            {
                Debug.Log(item.item.name + " " + "spawned");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }
}
