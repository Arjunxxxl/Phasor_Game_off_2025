using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    // Phases Data
    private PhasesType activePhase = PhasesType.Unknown;
    private List<PhasesType> availablePhases;
    
    // Phase Active Data
    private float phaseActiveTimeElapsed = 0.0f;
    private float phaseActiveDuration = 0.0f;
    private bool isPhaseCountdownActive = false;

    // Data Req For Phases
    private List<BoxCollider> LightBeamColliders = new List<BoxCollider>();
    private readonly string AstralPlaneTag = "AstralPlane";
    
    // Data
    private bool isGamePaused = false;
    
    // Ref
    private Player player;
    private UserInput userInput;
    private InfoPanelManager infoPanelManager;
    private PhaseUi phaseUi;
    private LocalDataManager localDataManager;
    private PostProcessingManager postProcessingManager;
    
    // Properties
    public PhasesType ActivePhase => activePhase;

    #region Singleton

    public static PhaseManager Instance;

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
    
    private void Update()
    {
        if (isGamePaused)
        {
            return;
        }
        
        ReadPhaseInput();
        TickPhaseCoolDownTimer();
    }

    #region SetUp

    public void SetUp(Player player)
    {
        userInput = UserInput.Instance;
        infoPanelManager = InfoPanelManager.Instance;
        localDataManager = LocalDataManager.Instance;
        postProcessingManager = PostProcessingManager.Instance;
        phaseUi = UiManager.Instance.GameplayUi.PhaseUi;
        
        this.player = player;

        isGamePaused = false;
        
        LoadAvailablePhases();
        SetUpDataReqForPhases();

        ActivatePhase(PhasesType.Default);
        
        phaseUi.SetUp(availablePhases);
    }

    private void LoadAvailablePhases()
    {
        availablePhases = new List<PhasesType>();
        availablePhases.Add(PhasesType.Default);

        for (int idx = 0; idx < System.Enum.GetValues(typeof(PhasesType)).Length; idx++)
        {
            PhasesType phasesType = (PhasesType) idx;
            bool isUnlocked = localDataManager.IsPhasesUnlocked(phasesType);
            if (isUnlocked)
            {
                availablePhases.Add(phasesType);
            }
        }
        
        
        /*availablePhases.Add(PhasesType.TimeShift);
       availablePhases.Add(PhasesType.Air);
       availablePhases.Add(PhasesType.Mirror);*/
    }

    private void SetUpDataReqForPhases()
    {
        GameObject[] astralGrounds = GameObject.FindGameObjectsWithTag(AstralPlaneTag);
        
        LightBeamColliders = new List<BoxCollider>();

        for (int i = 0; i < astralGrounds.Length; i++)
        {
            BoxCollider boxCollider = astralGrounds[i].GetComponent<BoxCollider>();
            LightBeamColliders.Add(boxCollider);
        }
    }

    #endregion

    #region Phase Activation / Deactivation

    private void ReadPhaseInput()
    {
        int phaseInputIdx = userInput.PhaseInput;
        
        if (phaseInputIdx < 0)
        {
            return;
        }
        
        PhasesType inputPhase = (PhasesType)phaseInputIdx;
        ActivatePhase(inputPhase);
    }
    
    private void ActivatePhase(PhasesType phase)
    {
        if (activePhase == phase)
        {
            Debug.Log("Trying to activate same phase" + phase);
            return;
        }
        
        if (!availablePhases.Contains(phase))
        {
            Debug.Log("Phase not available: " + phase);
            return;
        }
        
        DisablePhaseEffect(activePhase);
        
        activePhase = phase;
        
        phaseActiveTimeElapsed = 0.0f;
        phaseActiveDuration = Constants.PhaseData.PhaseActiveDuration;
        isPhaseCountdownActive = activePhase != PhasesType.Default;

        EnablePhaseEffect(activePhase);
        
        phaseUi.UpdateInActiveUi(phaseActiveTimeElapsed, phaseActiveDuration);
    }

    private void TickPhaseCoolDownTimer()
    {
        if (!isPhaseCountdownActive)
        {
            return;
        }
        
        phaseActiveTimeElapsed += Time.unscaledDeltaTime;
        
        phaseUi.UpdateInActiveUi(phaseActiveTimeElapsed, phaseActiveDuration);
        
        if (phaseActiveTimeElapsed >= phaseActiveDuration)
        {
            isPhaseCountdownActive = false;
            
            DisablePhaseEffect(activePhase);
            ActivatePhase(PhasesType.Default);
            
            phaseUi.RemoveInActiveUi();
        }
    }

    #endregion

    #region Phase Effect

    private void EnablePhaseEffect(PhasesType phase)
    {
        switch (phase)
        {
            case PhasesType.Default:
                break;
            
            case PhasesType.TimeShift:
                EnableTimeShiftEffect();
                
                AudioManager.Instance.PlayAudio(AudioClipType.TimeSlowPhaseActivated_1);
                break;
            
            case PhasesType.Air:
                EnableAirEffect();
                
                AudioManager.Instance.PlayAudio(AudioClipType.PhaseActivated);
                break;
            
            case PhasesType.AstralPlane:
                EnableAstralEffect();
                
                AudioManager.Instance.PlayAudio(AudioClipType.PhaseActivated);
                break;
            
            case PhasesType.Mirror:
                EnableMirrorEffect();
                
                AudioManager.Instance.PlayAudio(AudioClipType.PhaseActivated);
                break;
        }
    }

    private void DisablePhaseEffect(PhasesType phase)
    {
        switch (phase)
        {
            case PhasesType.Default:
                break;
            
            case PhasesType.TimeShift:
                DisableTimeShiftEffect();
                break;
            
            case PhasesType.Air:
                DisableAirEffect();
                break;
            
            case PhasesType.AstralPlane:
                DisableAstralEffect();
                break;
        }
    }
    
    #region Time Shift

    private void EnableTimeShiftEffect()
    {
        Time.timeScale = Constants.PhaseData.TimeShiftTimeSlow;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        
        postProcessingManager.PlayEffect();
    }

    private void DisableTimeShiftEffect()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        
        postProcessingManager.StopEffect();
    }

    #endregion

    #region Air

    private void EnableAirEffect()
    {
        player.playerMovement.SetHighJump(true);
    }

    private void DisableAirEffect()
    {
        player.playerMovement.SetHighJump(false);
    }

    #endregion
    
    #region Astral Plane

    private void EnableAstralEffect()
    {
        for (int idx = 0; idx < LightBeamColliders.Count; idx++)
        {
            LightBeamColliders[idx].isTrigger = false;
        }
    }

    private void DisableAstralEffect()
    {
        for (int idx = 0; idx < LightBeamColliders.Count; idx++)
        {
            LightBeamColliders[idx].isTrigger = true;
        }
    }

    #endregion

    #region Mirror

    private void EnableMirrorEffect()
    {
        player.playerMovement.ResetAllMovement();
        player.playerMovement.StopAllMovement(true);
        
        Transform playerT = player.transform;
        Vector3 forward = player.playerMovement.GetActualForwardDir();

        Vector3 landingPoint = playerT.position + forward * Constants.PhaseData.MirrorImageDist;
        
        playerT.position = landingPoint;
        
        StartCoroutine(EnableAllPlayerMovement());
    }

    IEnumerator EnableAllPlayerMovement()
    {
        yield return new WaitForFixedUpdate();
        player.playerMovement.StopAllMovement(false);
    }
    
    #endregion
    
    #endregion

    #region Phase Adding

    public void AddNewPhase(PhasesType phase)
    {
        if (!availablePhases.Contains(phase))
        {
            availablePhases.Add(phase);
            localDataManager.SavePhasesUnlocked(phase);

            StartCoroutine(ShowInfoPanel(phase));
        }
    }

    private IEnumerator ShowInfoPanel(PhasesType phase)
    {
        yield return new WaitForSeconds(1.5f);
        infoPanelManager.ShowInfoPanel(false, phase);
        
        phaseUi.UpdatePhaseUi(availablePhases);
    }

    #endregion

    #region Pausing Game

    public void HandleGamePause(bool isPaused)
    {
        isGamePaused = isPaused;
        
        if (!isPaused && activePhase == PhasesType.TimeShift)
        {
            EnableTimeShiftEffect();
        }
    }

    #endregion
}
