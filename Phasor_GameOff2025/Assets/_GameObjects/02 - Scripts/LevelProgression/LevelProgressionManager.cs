using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressionManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string introScene;
    [SerializeField] private string outroScene;
    [SerializeField] private List<string> levelScenes;

    private string curSceneName;
    private string nextSceneName;
    
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
    }

    #endregion
    
    private void Start()
    {
        curSceneName = SceneManager.GetActiveScene().name;

        if (curSceneName == introScene)
        {
            nextSceneName = levelScenes[0];
        }
        else if (curSceneName == levelScenes[^1])
        {
            nextSceneName = outroScene;
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
        }

        ExitLoadingScreen();
    }

    public void FirstLevel()
    {
        StartCoroutine(ShowLoadingScreenAndLoadNextLevelAfterDelay(levelScenes[0]));
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(ShowLoadingScreenAndLoadNextLevelAfterDelay(nextSceneName));
    }

    private IEnumerator ShowLoadingScreenAndLoadNextLevelAfterDelay(string levelName)
    {
        yield return new WaitForSeconds(3.0f);
        
        LoadingScreen.Instance.TriggerLoadingScreen();
        
        yield return new WaitForSeconds(1.0f);
        
        SceneManager.LoadSceneAsync(levelName);
    }

    private void ExitLoadingScreen()
    {
        LoadingScreen.Instance.ExitLoadingScreen();
    }
}
