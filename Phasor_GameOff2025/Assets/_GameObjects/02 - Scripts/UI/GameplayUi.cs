using System.Collections;
using UnityEngine;

public class GameplayUi : MonoBehaviour
{
    public GameObject checkPointReachedPanel;

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
}
