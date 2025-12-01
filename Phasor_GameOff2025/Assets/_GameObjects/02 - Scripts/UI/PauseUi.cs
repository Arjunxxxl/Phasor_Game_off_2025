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
        GameManager.Instance.ShowCursor();
        canvas.enabled = true;
    }

    public void HideMenu()
    {
        GameManager.Instance.HideCursor();
        canvas.enabled = false;
    }

    #endregion

    #region Button

    private void OnClickQuitButton()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.ButtonClick);
        
        Application.Quit();
    }

    private void OnClickResumeButton()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.ButtonClick);
        
        PauseManager.Instance.PauseResumeGame();
    }

    #endregion
}
