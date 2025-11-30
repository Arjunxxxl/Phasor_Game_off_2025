using System.Collections;
using UnityEngine;

public class GameplayUi : MonoBehaviour
{
    [Header("Panels")]
    public GameObject checkPointReachedPanel;
    
    [Header("Ref")]
    [SerializeField] private DiamondUi diamondUi;

    public void SetUp()
    {
        HideCheckPointReachedUi();
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

    #endregion
}
