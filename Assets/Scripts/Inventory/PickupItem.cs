using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewPickupItem", menuName = "Pickup/Item")]
public class PickupItem : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite itemIcon;
    public int rarity;

    public enum PickupType
    {
        Evidence,
        Battery,
        Cigarette,
        Key
    }

    public PickupType type;
    public int amount; // The quantity of this item to add to the inventory
}

