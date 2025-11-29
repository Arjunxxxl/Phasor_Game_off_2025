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

    private void Start()
    {
        localDataManager = LocalDataManager.Instance;
    }

    public void ShowInfoPanel(bool isCheckPointInfoPanel, PhasesType infoPanelPhaseType)
    {
        this.isCheckPointInfoPanel = isCheckPointInfoPanel;
        this.infoPanelPhaseType = infoPanelPhaseType;
        
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
    }

    public void InfoPanelHidden()
    {
        if (isCheckPointInfoPanel)
        {
            localDataManager.SaveCheckPointInfoShown(true);
        }
    }
}
