using System;
using UnityEngine;

public enum LoadingScreenState
{
    Unknown,
    Hidden,
    Enter,
    Wait,
    Exit
}

public class LoadingScreen : MonoBehaviour
{
    [Header("Animation Data")] 
    [SerializeField] private RectTransform bgRectT;
    [SerializeField] private float entryExitDur;
    private float inScreenPos;
    private float outScreenPos;
    
    // States
    private LoadingScreenState loadingScreenState = LoadingScreenState.Unknown;
    private float stateTimeElapsed;
    
    public static LoadingScreen Instance;
    
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
        
        inScreenPos = 0.0f;
        outScreenPos = Screen.width + 10;
        
        SetCurState(LoadingScreenState.Hidden);
    }

    private void Update()
    {
        UpdateStateTimer();
    }

    #region State

    private void SetCurState(LoadingScreenState newState)
    {
        if (newState == loadingScreenState)
        {
            return;
        }

        if (newState == LoadingScreenState.Hidden)
        {
            bgRectT.offsetMin = new Vector2(outScreenPos, 0.0f);
            bgRectT.offsetMax = new Vector2(outScreenPos, 0.0f);
            
            bgRectT.gameObject.SetActive(false);
        }
        else if (newState == LoadingScreenState.Enter)
        {
            bgRectT.offsetMin = new Vector2(outScreenPos, 0.0f);
            bgRectT.offsetMax = new Vector2(outScreenPos, 0.0f);
            
            bgRectT.gameObject.SetActive(true);
        }
        else if (newState == LoadingScreenState.Wait)
        {
            bgRectT.gameObject.SetActive(true);
        }
        else if (newState == LoadingScreenState.Exit)
        {
            bgRectT.gameObject.SetActive(true);
            
            bgRectT.offsetMin = new Vector2(inScreenPos, 0.0f);
            bgRectT.offsetMax = new Vector2(inScreenPos * -1, 0.0f);
        }
        
        loadingScreenState = newState;
        stateTimeElapsed = 0.0f;
    }

    private void UpdateStateTimer()
    {
        if (loadingScreenState == LoadingScreenState.Enter)
        {
            stateTimeElapsed += Time.deltaTime;
            float delta = stateTimeElapsed / entryExitDur;

            if (delta <= 1.05f)
            {
                bgRectT.offsetMin =
                    Vector2.Lerp(new Vector2(outScreenPos, 0.0f), new Vector2(inScreenPos, 0.0f), delta);
                bgRectT.offsetMax = Vector2.Lerp(new Vector2(outScreenPos, 0.0f), new Vector2(inScreenPos, 0.0f),
                    delta);
            }
        }
        else if (loadingScreenState == LoadingScreenState.Wait)
        {
            stateTimeElapsed += Time.deltaTime;
            float delta = stateTimeElapsed / entryExitDur;

            if (delta > 1.0f)
            {
                SetCurState(LoadingScreenState.Exit);
            }
        }
        else if (loadingScreenState == LoadingScreenState.Exit)
        {
            stateTimeElapsed += Time.deltaTime;
            float delta = stateTimeElapsed / entryExitDur;

            if (delta <= 1.05f)
            {
                bgRectT.offsetMin =
                    Vector2.Lerp(new Vector2(inScreenPos, 0.0f), new Vector2(outScreenPos * -1, 0.0f), delta);
                bgRectT.offsetMax = Vector2.Lerp(new Vector2(inScreenPos, 0.0f), new Vector2(outScreenPos * -1, 0.0f),
                    delta);
            }
            else
            {
                SetCurState(LoadingScreenState.Hidden);
            }
        }
    }

    #endregion
}
