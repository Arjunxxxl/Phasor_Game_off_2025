using UnityEngine;
using UnityEngine.UI;

public class GameOverUi : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    
    private Canvas canvas;
    
    #region SetUp

    public void SetUp()
    {
        canvas = GetComponent<Canvas>();
        
        restartButton.onClick.AddListener(OnClickRestartButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
        
        HideMenu();
    }

    #endregion
    
    #region Show Hide

    public void ShowMenu()
    {
        canvas.enabled = true;
    }

    private void HideMenu()
    {
        canvas.enabled = false;
    }

    #endregion

    #region Button

    private void OnClickQuitButton()
    {
        Application.Quit();
    }

    private void OnClickRestartButton()
    {
        LocalDataManager.Instance.ResetHeartLeftData(true);
        LevelProgressionManager.Instance.LoadFirstLevel();
    }

    #endregion
}
