using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    
    #region Singleton
    
    public static PauseManager Instance;

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

    // Player
    private Player player;
    private PhaseManager phaseManager;
    private PauseUi pauseUi;
    
    #region SetUp

    public void SetUp()
    {
        player = Player.Instance;
        phaseManager = PhaseManager.Instance;
        pauseUi = UiManager.Instance.PauseUi;
        
        isPaused = false;
        UpdateTimeScale(1.0f);
    }

    #endregion

    #region Pause / Resume Game

    public void PauseResumeGame()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
        
        phaseManager.HandleGamePause(isPaused);
    }
    
    private void PauseGame()
    {
        isPaused = true;
        
        player.playerMovement.StopAllMovement(true);
        UpdateTimeScale(0.0f);
        
        pauseUi.ShowMenu();
    }

    private void ResumeGame()
    {
        isPaused = false;
        
        player.playerMovement.StopAllMovement(false);
        UpdateTimeScale(1.0f);
        
        pauseUi.HideMenu();
    }

    #endregion
    
    #region Update Time Scale

    private void UpdateTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    #endregion
}
