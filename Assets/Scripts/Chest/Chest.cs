using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Serializable class for defining items in the chest
[Serializable]
public class ChestItems
{
    public GameObject item; // The item GameObject
    [Range(0, 1)]
    public float probability; // The probability of the item being spawned
}

// Class representing the Chest behavior
public class Chest : MonoBehaviour
{
    [SerializeField] private KeyCode searchKey; // The key used to search the chest
    [SerializeField] private float searchSpeed = 10.0f; // The speed at which the search progress increases
    [SerializeField] private float searchBoost = 0.5f; // The boost applied to the search progress when the key is pressed
    [SerializeField] private float searchDecreaseSpeed = 5.0f; // The speed at which the search progress decreases
    [SerializeField] private float buttonHoldThreshold = 0.1f; // The threshold for holding the search key
    [SerializeField] private Image radialLoaderUI; // The UI image representing the search progress
    [SerializeField] List<ChestItems> chestItems; // The list of items in the chest
    private float currentSearchProgress = 0.0f; // The current search progress
    private bool isPlayerInRange = false; // Flag indicating if the player is in range of the chest
    private bool isSearchButtonPressed = false; // Flag indicating if the search button is pressed
    private Coroutine searchCoroutine = null; // The coroutine for handling the search
    private float searchButtonReleaseTime = 0.0f; // The time when the search button was released
    private bool isSearchSuccessfull = false; // Flag indicating if the search was successful
    private bool searchBoosted = false; // Flag indicating if the search was boosted

    public GameObject chestItem;
    public Transform chestItemSpawnPoint;

    public Dialogue.Dialogue dlg;
    int npcIndex = 1;
    GenericNPC npc;
    public bool isTalking, canContinue;
    public GameObject loaderUI;
    public bool searched = false;
    PlayerController player;
    private void Start()
    {
        chestItemSpawnPoint = chestItem.transform;
        npc = gameObject.GetComponent<GenericNPC>();
        dlg = gameObject.GetComponent<Dialogue.Dialogue>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    }
    // Update is called once per frame
    private void Update()
    {
        if (!isPlayerInRange) return;

       
        isTalking = dlg.isTalking;
        canContinue = dlg.canContinue;


        if (Input.GetKeyDown(searchKey) && !isTalking && searched)
        {
            dlg.StartDialogue(npcIndex);


        }

        if (Input.GetKeyDown(searchKey) && canContinue)
            {
                dlg.NextSentence();
            }


            HandleChestInputs();
    }

    // Handles the inputs for searching the chest
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

    // Coroutine for handling the search progress
    private IEnumerator SearchCoroutine()
    {
        while (isSearchButtonPressed || Time.time - searchButtonReleaseTime <= buttonHoldThreshold && !isSearchSuccessfull)
        {
            player.canMove = false;
            currentSearchProgress += CalculateSearchProgress();
            radialLoaderUI.fillAmount = currentSearchProgress / 100.0f;
            if (currentSearchProgress >= 100)
            {
                OpenChest();
                
                yield break;
            }
            yield return null;
            player.canMove = true;
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

    // Calculates the search progress based on the current conditions
    private float CalculateSearchProgress()
    {
        float progress = searchSpeed * Time.deltaTime;
        if (!searchBoosted && isSearchButtonPressed)
        {
            progress += searchBoost;
            searchBoosted = true;
            //Debug.Log("Chest Smashed");
        }

        return progress;
    }

    // Opens the chest and spawns the items
    private void OpenChest()
    {
        isSearchButtonPressed = false;
        isSearchSuccessfull = true;
        currentSearchProgress = 100.0f;
        //TODO: Remove this
        // GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("Chest Opened");

        SpawChestItems();
    }

    // Spawns the items in the chest based on their probabilities
    private void SpawChestItems()
    {
        if (chestItems.Count == 0 || searched == true)
        {
            Debug.Log("No items in the chest");
            StartConversation();
        }
        else if (chestItems.Count >= 1)
        {
            npcIndex = 2;
            foreach (ChestItems item in chestItems)
            {
                Instantiate(item.item, chestItemSpawnPoint.position, Quaternion.identity);
            }
            StartConversation();
        }
        loaderUI.SetActive(false);

        searched = true;
    }


        public void StartConversation()
        {
            dlg.StartDialogue(npcIndex);
        }

        // Handles the trigger enter event
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                isPlayerInRange = true;
            }
        }

        // Handles the trigger exit event
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                isPlayerInRange = false;
            }
        }
    
}

