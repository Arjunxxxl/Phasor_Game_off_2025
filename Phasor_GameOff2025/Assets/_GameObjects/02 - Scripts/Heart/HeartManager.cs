using System.Collections;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    private int heartsLeft;

    // Ref
    private LocalDataManager localDataManager;
    private HeartUi heartUi;
    
    #region Singleton
    
    public static HeartManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    #region SetUp

    public void SetUp()
    {
        localDataManager = LocalDataManager.Instance;
        heartUi = UiManager.Instance.GameplayUi.HeartUi;
        
        heartsLeft = localDataManager.GetHeartLeft();

        heartUi.SetUp(heartsLeft);
    }

    #endregion

    #region Consume Hearts

    public bool ConsumeOneHeart()
    {
        heartsLeft--;

        heartUi.UpdateHeartUi(heartsLeft);
        localDataManager.SaveHeartLeftData(heartsLeft);
        
        if (heartsLeft <= 0)
        {
            StartCoroutine(ShowGameOverUi());
            return true;
        }

        return false;
    }

    private IEnumerator ShowGameOverUi()
    {
        yield return new WaitForSeconds(1.0f);
        UiManager.Instance.GameOverUi.ShowMenu();
    }

    #endregion
}
