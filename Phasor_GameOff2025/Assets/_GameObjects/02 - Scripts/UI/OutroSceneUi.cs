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
        Application.Quit();
    }

    private void OnClickRestartButton()
    {
        LocalDataManager.Instance.ClearAllData();
        LocalDataManager.Instance.ResetHeartLeftData(true);
        LocalDataManager.Instance.ResetLevelNameData();
        
        LevelProgressionManager.Instance.LoadFirstLevel();
    }

    #endregion
}
