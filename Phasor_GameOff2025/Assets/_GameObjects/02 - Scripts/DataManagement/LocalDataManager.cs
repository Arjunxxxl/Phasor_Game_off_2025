using UnityEngine;

public class LocalDataManager : MonoBehaviour
{
    private int levelNumber = 0;
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
        LoadLevelNumberData();
        LoadCheckPointInfoData();
        LoadPhasesData();
    }
    
    #region Level

    private void LoadLevelNumberData()
    {
        levelNumber = PlayerPrefs.GetInt(Constants.LocalData.LevelNumber_Tag, 0);
    }

    public void SaveLevelNumberData(int levelNumber)
    {
        this.levelNumber = levelNumber;
        PlayerPrefs.SetInt(Constants.LocalData.LevelNumber_Tag, levelNumber);
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

    public bool IsCheckPointInfoShown()
    {
        return checkPointInfoShown;
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
    
    public bool IsPhasesUnlockedInfoShown(PhasesType phasesType)
    {
        bool shown = false;
        switch (phasesType)
        {
            case PhasesType.TimeShift:
                shown = timeShiftInfoShown;
                break;
        }
        
        return shown;
    }
    
    #endregion
}
