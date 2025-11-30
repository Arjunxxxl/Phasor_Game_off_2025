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
            // TODO: game over
            return true;
        }

        return false;
    }

    #endregion
}
