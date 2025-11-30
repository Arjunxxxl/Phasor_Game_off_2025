using UnityEngine;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    
    private Canvas canvas;
    
    #region SetUp

    public void SetUp()
    {
        canvas = GetComponent<Canvas>();
        
        resumeButton.onClick.AddListener(OnClickResumeButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
        
        HideMenu();
    }

    #endregion
    
    #region Show Hide

    public void ShowMenu()
    {
        canvas.enabled = true;
    }

    public void HideMenu()
    {
        canvas.enabled = false;
    }

    #endregion

    #region Button

    private void OnClickQuitButton()
    {
        Application.Quit();
    }

    private void OnClickResumeButton()
    {
        PauseManager.Instance.PauseResumeGame();
    }

    #endregion
}
