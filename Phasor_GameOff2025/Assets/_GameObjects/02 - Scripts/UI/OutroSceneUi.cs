using System;
using UnityEngine;
using UnityEngine.UI;

public class OutroSceneUi : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        restartButton.onClick.AddListener(OnClickRestartButton);
        quitButton.onClick.AddListener(OnClickQuitButton);
    }

    #region Button

    private void OnClickQuitButton()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.ButtonClick);
        
        Application.Quit();
    }

    private void OnClickRestartButton()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.ButtonClick);
        
        LocalDataManager.Instance.ClearAllData();
        LocalDataManager.Instance.ResetHeartLeftData(true);
        LocalDataManager.Instance.ResetLevelNameData();
        
        LevelProgressionManager.Instance.LoadFirstLevel();
    }

    #endregion
}
