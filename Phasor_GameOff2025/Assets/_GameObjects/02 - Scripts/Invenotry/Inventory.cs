using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Items in inventory
    private Dictionary<InventoryItemType, int> CarryingItems = new Dictionary<InventoryItemType, int>();

    #region Singleton

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    #region Inventory Operations

    public void AddItemToInventory(InventoryItemType type, int amount)
    {
        if (!CarryingItems.TryAdd(type, amount))
        {
            CarryingItems[type] += amount;
        }
    }

    public void RemoveItemFromInventory(InventoryItemType type, int amount)
    {
        if (CarryingItems.ContainsKey(type))
        {
            CarryingItems[type] -= amount;
        }
    }

    public int GetAmount(InventoryItemType type)
    {
        return CarryingItems.GetValueOrDefault(type, 0);
    }

    #endregion
}
