using UnityEngine;
using UnityEngine.UI;

public class HeartIndicator : MonoBehaviour
{
    [Header("UI")] 
    public Image heartIcon;
    public GameObject glowGo;
    public GameObject containerGo;

    [Header("Sprite")] 
    public Sprite activeSprite;
    public Sprite inActiveSprite;
    
    public void SetHeartActivation(bool isActive)
    {
        if (isActive)
        {
            heartIcon.sprite = activeSprite;
            glowGo.transform.SetSiblingIndex(1);
        }
        else
        {
            heartIcon.sprite = inActiveSprite;
            glowGo.transform.SetSiblingIndex(0);
        }
    }
}
