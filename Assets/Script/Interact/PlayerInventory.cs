using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<string> inventory = new List<string>();

    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
    }

    public void RemoveItem(string itemName)
    {
        inventory.Remove(itemName);
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }
}