using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressionManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private List<string> levelScenes;
    private readonly string introLevel = Constants.SceneData.StartingLevelName;
    private readonly string outroLevel = Constants.SceneData.OutroLevelName;

    private string curSceneName;
    private string nextSceneName;
    
    public string CurSceneName => curSceneName;
    
    private LocalDataManager localDataManager;
    
    #region Singleton

    public static LevelProgressionManager Instance;

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
        
        levelScenes = new List<string>
        {
            "Level 1",
            "Level 2",
            "Level 3",
            "Level 4",
            "Level 5",
            "Level 6"
        };
    }

    #endregion
    
    public void SetUp()
    {
        localDataManager = LocalDataManager.Instance;
        
        curSceneName = SceneManager.GetActiveScene().name;

        if (curSceneName == introLevel)
        {
            string savedLevelName = localDataManager.GetLevelName();

            nextSceneName = savedLevelName == Constants.SceneData.StartingLevelName ? levelScenes[0] : savedLevelName;
        }
        else if (curSceneName == levelScenes[^1])
        {
            nextSceneName = outroLevel;
        }
        else if (curSceneName == outroLevel)
        {
            nextSceneName = Constants.SceneData.StartingLevelName;
        }
        else
        {
            for (int idx = 0; idx < levelScenes.Count - 1; idx++)
            {
                if (curSceneName == levelScenes[idx])
                {
                    nextSceneName = levelScenes[idx + 1];
                }
            }
            
            localDataManager.SaveLevelNameData(curSceneName);
        }

        ExitLoadingScreen();
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(ShowLoadingScreenAndLoadNextLevelAfterDelay(levelScenes[0], 0.0f));
    }

    public void LoadLevelFromStartingLevel()
    {
        StartCoroutine(ShowLoadingScreenAndLoadNextLevelAfterDelay(nextSceneName, 0.0f));
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(ShowLoadingScreenAndLoadNextLevelAfterDelay(nextSceneName, 3.0f));
    }

    private IEnumerator ShowLoadingScreenAndLoadNextLevelAfterDelay(string levelName, float startingDelay)
    {
        yield return new WaitForSeconds(startingDelay);
        
        LoadingScreen.Instance.TriggerLoadingScreen();
        
        yield return new WaitForSeconds(1.0f);
        
        SceneManager.LoadSceneAsync(levelName);
    }

    private void ExitLoadingScreen()
    {
        LoadingScreen.Instance.ExitLoadingScreen();
    }
}
