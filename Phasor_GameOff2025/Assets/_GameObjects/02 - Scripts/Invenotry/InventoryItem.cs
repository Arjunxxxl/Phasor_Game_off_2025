using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private InventoryItemType itemType;
    
    public InventoryItemType ItemType => itemType;

    public void OnItemPickedUp()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.KeyCollected);
        
        gameObject.SetActive(false);
    }
}
