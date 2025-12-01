using System.Collections;
using TMPro;
using UnityEngine;

public class GameplayUi : MonoBehaviour
{
    [Header("Panels")]
    public GameObject checkPointReachedPanel;
    public TMP_Text levelNumberText;
    
    [Header("Ref")]
    [SerializeField] private DiamondUi diamondUi;
    [SerializeField] private HeartUi heartUi;
    [SerializeField] private PhaseUi phaseUi;

    public void SetUp()
    {
        HideCheckPointReachedUi();

        levelNumberText.text = LevelProgressionManager.Instance.CurSceneName;
    }

    #region CheckPoint Reached Ui

    public void ShowCheckPointReachUi()
    {
        checkPointReachedPanel.SetActive(true);

        StartCoroutine(HideCheckPointReachedUiAfterDelay());
    }

    private void HideCheckPointReachedUi()
    {
        checkPointReachedPanel.SetActive(false);
    }

    private IEnumerator HideCheckPointReachedUiAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        HideCheckPointReachedUi();
    }

    #endregion

    #region Getters

    public DiamondUi DiamondUi => diamondUi;
    public HeartUi HeartUi => heartUi;
    public PhaseUi PhaseUi => phaseUi;

    #endregion
}
