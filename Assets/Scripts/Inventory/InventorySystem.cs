using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static Evidence;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventory;

    bool inventoryEnabled = false;

    bool cigSelected = false, battSelected = false, keySelected = false;

    public int battAmount, cigAmount, keyAmount;
    [SerializeField] TextMeshProUGUI battText, cigText, keyText;

    [SerializeField] Image selectedItemImage;
    [SerializeField] TextMeshProUGUI itemDesc;
    [SerializeField] TextMeshProUGUI itemName;


    [SerializeField] private Transform gridParent; 
    [SerializeField] private GameObject buttonPrefab;

    SanityController sanityController;
    FlashlightController flashlightController;

    [SerializeField]GameObject cigButton,battButton;

    private void Start()
    {
        sanityController = GameObject.FindGameObjectWithTag("Player").GetComponent<SanityController>();

        flashlightController = GameObject.FindGameObjectWithTag("Player").GetComponent<FlashlightController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }


        battText.text = battAmount.ToString();
        cigText.text = cigAmount.ToString();
        keyText.text = keyAmount.ToString();
    }

    public void SelectCig() 
    {
        selectedItemImage.sprite = Resources.Load<Sprite>("Cigarette");
        itemDesc.text = "A cigarette. It's a bad habit, but it's the only thing that keeps you sane in this place.";
        itemName.text = "Cigarette";

        if (!cigButton.activeSelf)
        {
            cigButton.SetActive(true);
        }

        if (battButton.activeSelf)
        {
            battButton.SetActive(false);
        }


    }

    public void SmokeCig()
    {
        if (cigAmount > 0)
        {
            cigAmount--;
            sanityController.AddSanity(10);
        }
        
    }

    public void SelectBattery()
    {
        selectedItemImage.sprite = Resources.Load<Sprite>("Battery");
        itemDesc.text = "A battery. It's a bit rusty, but it should still work.";
        itemName.text = "Battery";


        if (cigButton.activeSelf)
        {
            cigButton.SetActive(false);
        }

        if (!battButton.activeSelf)
        {
            battButton.SetActive(true);
        }
    }

    public void UseBattery()
    {
        if (battAmount > 0)
        {
            battAmount--;
            flashlightController.AddPower(100);
        }
    }

    public void SelectKey()
    {
        selectedItemImage.sprite = Resources.Load<Sprite>("Key");
        itemDesc.text = "A key. Who knows what it unlocks.";
        itemName.text = "Key";

        if (cigButton.activeSelf)
        {
            cigButton.SetActive(false);
        }

        if (battButton.activeSelf)
        {
            battButton.SetActive(false);
        }
    }

    public void AddEvidence(EvidenceItem evidenceItem)
    {
        GameObject newButton = Instantiate(buttonPrefab, gridParent);

        Image buttonIcon = newButton.GetComponentInChildren<Image>();
        if (buttonIcon != null)
        {
            buttonIcon.sprite = evidenceItem.evidenceIcon;
        }

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => UpdateEvidenceDetails(evidenceItem));
    }

    private void UpdateEvidenceDetails(EvidenceItem evidenceItem)
    {
        itemName.text = $"{evidenceItem.evidenceName}";
        itemDesc.text = $"{evidenceItem.evidenceDescription}";
        selectedItemImage.sprite = evidenceItem.evidenceIcon;

        if (cigButton.activeSelf)
        {
            cigButton.SetActive(false);
        }

        if (battButton.activeSelf)
        {
            battButton.SetActive(false);
        }
    }

}
