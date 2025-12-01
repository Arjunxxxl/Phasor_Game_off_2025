using System;
using System.IO;
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

        HideCursor();
    }

    private void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveScreenshot();
        }
        #endif
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    private void SaveScreenshot()
    {
        // Create a timestamp string
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

        // Create folder if it doesn't exist
        string folderPath = Path.Combine(Application.persistentDataPath, "Screenshots");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Full file path
        string filePath = Path.Combine(folderPath, $"screenshot_{timestamp}.png");

        // Take screenshot
        ScreenCapture.CaptureScreenshot(filePath);

        Debug.Log("Screenshot saved at: " + filePath);
    }
}
