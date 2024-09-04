using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<string> inventory = new List<string>();

    public void AddItem(string itemName)
    {
        Debug.Log(itemName + "��(��) �κ��丮�� �߰��Ǿ����ϴ�.");
        inventory.Add(itemName);
    }

    public void RemoveItem(string itemName)
    {
        Debug.Log(itemName + "��(��) �κ��丮�� ���ŵǾ����ϴ�.");
        inventory.Remove(itemName);
    }

    public bool HasItem(string itemName)
    {
        Debug.Log(itemName + "��(��) �����ϰ� �ִ��� Ȯ���մϴ�.");
        return inventory.Contains(itemName);
    }
}
