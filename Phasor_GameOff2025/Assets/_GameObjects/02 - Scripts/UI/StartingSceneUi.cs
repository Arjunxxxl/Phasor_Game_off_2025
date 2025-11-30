using System;
using UnityEngine;
using UnityEngine.UI;

public class StartingSceneUi : MonoBehaviour
{
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
    }

    private void OnClickPlayButton()
    {
        LevelProgressionManager.Instance.LoadLevelFromStartingLevel();
    }
}
