using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UiManager uiManager;
    private InfoPanelManager infoPanelManager;
    private Player player;
    private CheckPointManager checkPointManager;
    private PhaseManager phaseManager;
    private DiamondManager diamondManager;
    private HeartManager heartManager;
    private PauseManager pauseManager;
    private LevelProgressionManager levelProgressionManager;
    
    #region Singleton
    
    public static GameManager Instance;

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
        SetUpGame();
    }

    private void SetUpGame()
    {
        uiManager = UiManager.Instance;
        infoPanelManager = InfoPanelManager.Instance;
        player = Player.Instance;
        checkPointManager = CheckPointManager.Instance;
        phaseManager = PhaseManager.Instance;
        diamondManager = DiamondManager.Instance;
        heartManager = HeartManager.Instance;
        pauseManager = PauseManager.Instance;
        levelProgressionManager = LevelProgressionManager.Instance;
        
        levelProgressionManager.SetUp();
        
        uiManager.SetUp();
        
        infoPanelManager.SetUp();
        checkPointManager.SetUp();
        player.SetUp();
        phaseManager.SetUp(player);
        diamondManager.SetUp();
        heartManager.SetUp();
        pauseManager.SetUp();
    }
}
