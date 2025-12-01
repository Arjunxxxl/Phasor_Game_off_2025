using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    private int totalDiamondCollected = 0;
    private DiamondUi diamondUi;

    #region Singleton

    public static DiamondManager Instance;

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
    
    #region SetUp

    public void SetUp()
    {
        diamondUi = UiManager.Instance.GameplayUi.DiamondUi;
        
        totalDiamondCollected = 0;
        UpdateDiamondTxt();
    }

    #endregion

    #region Diamond Collection

    public void DiamondCollected()
    {
        totalDiamondCollected++;
        UpdateDiamondTxt();
        
        AudioManager.Instance.PlayAudio(AudioClipType.DiamondCollect, true);
    }

    #endregion

    private void UpdateDiamondTxt()
    {
        diamondUi.UpdateDiamondTxt(totalDiamondCollected);
    }
}
