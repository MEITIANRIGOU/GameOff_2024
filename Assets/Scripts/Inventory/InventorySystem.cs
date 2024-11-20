using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventory;

    private bool inventoryEnabled = false;

    public int battAmount, cigAmount, keyAmount;
    [SerializeField] private TextMeshProUGUI battText, cigText, keyText;

    [SerializeField] private Image selectedItemImage;
    [SerializeField] private TextMeshProUGUI itemDesc;
    [SerializeField] private TextMeshProUGUI itemName;

    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject buttonPrefab;

    private SanityController sanityController;
    private FlashlightController flashlightController;

    [SerializeField] private GameObject cigButton, battButton;

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
            inventory.SetActive(inventoryEnabled);
        }

        battText.text = battAmount.ToString();
        cigText.text = cigAmount.ToString();
        keyText.text = keyAmount.ToString();
    }

    public void SelectItem(PickupItem item)
    {
        if (item == null)
        {
            Debug.LogError("Item is null");
            return;
        }

        selectedItemImage.sprite = item.itemIcon;
        itemDesc.text = item.itemDescription;
        itemName.text = item.itemName;


        cigButton.SetActive(item.type == PickupItem.PickupType.Cigarette);
        battButton.SetActive(item.type == PickupItem.PickupType.Battery);
    }

    public void UseCigarette()
    {
        if (cigAmount > 0)
        {
            cigAmount--;
            sanityController.AddSanity(10);
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

    public void AddEvidence(PickupItem evidenceItem)
    {
        if (evidenceItem == null)
        {
            Debug.LogError("No evidence item provided!");
            return;
        }

        GameObject newButton = Instantiate(buttonPrefab, gridParent);
        Image buttonIcon = newButton.GetComponentInChildren<Image>();
        if (buttonIcon != null)
        {
            buttonIcon.sprite = evidenceItem.itemIcon;
        }

        Button button = newButton.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => SelectItem(evidenceItem));
        }
    }
}

   


