using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Ui Refs")]
    [SerializeField] private GameplayUi gameplayUi;
    [SerializeField] private InfoPanel infoPanel;
    
    public GameplayUi GameplayUi => gameplayUi;
    
    #region Singleton
    
    public static UiManager Instance;

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

    public void SetUp()
    {
        gameplayUi.SetUp();
        infoPanel.SetUp();
    }
}
