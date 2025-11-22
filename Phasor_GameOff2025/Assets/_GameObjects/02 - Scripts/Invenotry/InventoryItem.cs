using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryItemType itemType;
    
    public InventoryItemType ItemType => itemType;

    public void OnItemPickedUp()
    {
        gameObject.SetActive(false);
    }
}
