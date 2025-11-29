using System;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    [SerializeField] private InfoPanel infoPanel;
    [SerializeField] private InfoPanelData infoPanelData;
    
    private bool isCheckPointInfoPanel = false;
    private PhasesType infoPanelPhaseType = PhasesType.Unknown;
    
    // Ref
    private LocalDataManager localDataManager;
    private Player player;
    
    #region Singleton
    
    public static InfoPanelManager Instance;

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

    public void SetUp()
    {
        localDataManager = LocalDataManager.Instance;
        player = Player.Instance;
    }

    public void ShowInfoPanel(bool isCheckPointInfoPanel, PhasesType infoPanelPhaseType)
    {
        this.isCheckPointInfoPanel = isCheckPointInfoPanel;
        this.infoPanelPhaseType = infoPanelPhaseType;

        if (this.isCheckPointInfoPanel && localDataManager.IsCheckPointInfoShown())
        {
            return;
        }
        else if (localDataManager.IsPhasesUnlocked(infoPanelPhaseType))
        {
            return;
        }
        
        InfoPanelData.Data data = null;
        
        if (isCheckPointInfoPanel)
        {
            data = infoPanelData.GetInfoPanelData(0);
        }
        else if (infoPanelPhaseType != PhasesType.Unknown)
        {
            data = infoPanelData.GetInfoPanelData((int)infoPanelPhaseType);
        }
        
        infoPanel.SetPanelData(data);
        infoPanel.ShowMenu();
        
        Time.timeScale = 0.0f;
        player.playerMovement.StopAllMovement(true);
    }

    public void InfoPanelHidden()
    {
        if (isCheckPointInfoPanel)
        {
            localDataManager.SaveCheckPointInfoShown(true);
        }
        
        Time.timeScale = 1.0f;
        player.playerMovement.StopAllMovement(false);
    }
}
