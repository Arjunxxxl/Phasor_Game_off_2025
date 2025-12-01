using UnityEngine;

public class LocalDataManager : MonoBehaviour
{
    private string levelName = "";
    private int heartsLeft = 0;
    private bool checkPointInfoShown = false;
    private bool timeShiftInfoShown = false;
    private bool airInfoShown = false;
    private int totalPhasesUnlocked = 0;
    
    #region Singleton
    
    public static LocalDataManager Instance;

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
        
        DontDestroyOnLoad(gameObject);

        LoadAllData();
    }

    #endregion

    private void LoadAllData()
    {
        LoadLevelNameData();
        LoadHeartLeftData();
        LoadCheckPointInfoData();
        LoadPhasesData();
    }

    public void ClearAllData()
    {
        ResetCheckPointShown();
        ResetPhasesUnlocked();
        
        PlayerPrefs.DeleteAll();
    }
    
    #region Level

    private void LoadLevelNameData()
    {
        levelName = PlayerPrefs.GetString(Constants.LocalData.LevelName_Tag, Constants.SceneData.StartingLevelName);
    }

    public void SaveLevelNameData(string levelName)
    {
        this.levelName = levelName;
        PlayerPrefs.SetString(Constants.LocalData.LevelName_Tag, levelName);
    }

    public void ResetLevelNameData()
    {
        levelName = Constants.SceneData.StartingLevelName;
        PlayerPrefs.SetString(Constants.LocalData.LevelName_Tag, levelName);
    }

    public string GetLevelName()
    {
        return levelName;
    }
    
    #endregion
    
    #region Check Point

    private void LoadCheckPointInfoData()
    {
        checkPointInfoShown = PlayerPrefs.GetInt(Constants.LocalData.CheckPointInfoShown_Tag, 0) == 1;
    }

    public void SaveCheckPointInfoShown(bool isShown)
    {
        this.checkPointInfoShown = isShown;
        PlayerPrefs.SetInt(Constants.LocalData.CheckPointInfoShown_Tag, isShown ? 1 : 0);
    }

    private void ResetCheckPointShown()
    {
        this.checkPointInfoShown = false;
        PlayerPrefs.SetInt(Constants.LocalData.CheckPointInfoShown_Tag, 0);
    }

    public bool IsCheckPointInfoShown()
    {
        return checkPointInfoShown;
    }
    
    #endregion
    
    #region Heart

    private void LoadHeartLeftData()
    {
        heartsLeft = PlayerPrefs.GetInt(Constants.LocalData.HeartsLeft_Tag, Constants.Player.MaxHeartCt);
        
        ResetHeartLeftData();
    }

    public void SaveHeartLeftData(int heartsLeft)
    {
        this.heartsLeft = heartsLeft;
        PlayerPrefs.SetInt(Constants.LocalData.HeartsLeft_Tag, heartsLeft);

        ResetHeartLeftData();
    }

    public int GetHeartLeft()
    {
        return heartsLeft;
    }

    public void ResetHeartLeftData(bool forceReset = false)
    {
        if (heartsLeft <= 0 || forceReset)
        {
            PlayerPrefs.SetInt(Constants.LocalData.HeartsLeft_Tag, Constants.Player.MaxHeartCt);
            heartsLeft = Constants.Player.MaxHeartCt;
        }
    }
    
    #endregion
    
    #region Phases

    private void LoadPhasesData()
    {
        timeShiftInfoShown = PlayerPrefs.GetInt(Constants.LocalData.TimeShiftInfoShown_Tag, 0) == 1;
        airInfoShown = PlayerPrefs.GetInt(Constants.LocalData.AirInfoShown_Tag, 0) == 1;
        
        totalPhasesUnlocked = PlayerPrefs.GetInt(Constants.LocalData.TotalPhasesUnlocked_Tag, 0);
    }

    public void SavePhasesUnlocked(PhasesType phasesType)
    {
        totalPhasesUnlocked = (int) phasesType;
        PlayerPrefs.SetInt(Constants.LocalData.TotalPhasesUnlocked_Tag, totalPhasesUnlocked);

        switch (phasesType)
        {
            case PhasesType.TimeShift:
                timeShiftInfoShown = true;
                PlayerPrefs.SetInt(Constants.LocalData.TimeShiftInfoShown_Tag, 1);
                break;
        }
    }
    
    public bool IsPhasesUnlocked(PhasesType phasesType)
    {
        bool isUnlocked = false;
        switch (phasesType)
        {
            case PhasesType.TimeShift:
                isUnlocked = timeShiftInfoShown;
                break;
        }
        
        return isUnlocked;
    }
    
    private void ResetPhasesUnlocked()
    {
        PlayerPrefs.SetInt(Constants.LocalData.TimeShiftInfoShown_Tag, 0);
        PlayerPrefs.SetInt(Constants.LocalData.AirInfoShown_Tag, 0);
        
        PlayerPrefs.SetInt(Constants.LocalData.TotalPhasesUnlocked_Tag, 0);
    }
    
    #endregion
}
