using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Ui Refs")]
    [SerializeField] private GameplayUi gameplayUi;
    [SerializeField] private GameOverUi gameOverUi;
    [SerializeField] private InfoPanel infoPanel;
    [SerializeField] private PauseUi pauseUi;
    
    public GameplayUi GameplayUi => gameplayUi;
    public GameOverUi GameOverUi => gameOverUi;
    public PauseUi PauseUi => pauseUi;
    
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
        gameOverUi.SetUp();
        infoPanel.SetUp();
        pauseUi.SetUp();
    }
}
