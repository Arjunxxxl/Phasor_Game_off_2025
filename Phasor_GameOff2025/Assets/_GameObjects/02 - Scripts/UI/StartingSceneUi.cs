using System;
using UnityEngine;
using UnityEngine.UI;

public class StartingSceneUi : MonoBehaviour
{
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(OnClickPlayButton);

        LevelProgressionManager.Instance.SetUp();
    }

    private void OnClickPlayButton()
    {
        AudioManager.Instance.PlayAudio(AudioClipType.ButtonClick);
        
        LevelProgressionManager.Instance.LoadLevelFromStartingLevel();
    }
}
