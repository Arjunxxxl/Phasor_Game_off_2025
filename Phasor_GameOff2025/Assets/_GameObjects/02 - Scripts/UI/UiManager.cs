using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Ui Refs")]
    [SerializeField] private InfoPanel infoPanel;
    
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
        infoPanel.SetUp();
    }
}
