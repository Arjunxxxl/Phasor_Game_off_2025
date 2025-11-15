using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    // Phases Data
    private PhasesType activePhase;
    private List<PhasesType> availablePhases;
    
    // Phase Active Data
    private float phaseActiveTimeElapsed = 0.0f;
    private float phaseActiveDuration = 0.0f;
    private bool isPhaseCountdownActive = false;

    // Data Req For Phases
    private List<BoxCollider> LightBeamColliders = new List<BoxCollider>();
    private readonly string AstralPlaneTag = "AstralPlane";
    
    // Ref
    private Player player;
    private UserInput userInput;
    
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
        ReadPhaseInput();
        TickPhaseCoolDownTimer();
    }

    #region SetUp

    public void SetUp(Player player)
    {
        userInput = UserInput.Instance;
        
        this.player = player;

        LoadAvailablePhases();
        SetUpDataReqForPhases();

        ActivatePhase(PhasesType.Default);
    }

    private void LoadAvailablePhases()
    {
        availablePhases = new List<PhasesType>();
        availablePhases.Add(PhasesType.Default);
        availablePhases.Add(PhasesType.TimeShift);
        availablePhases.Add(PhasesType.Air);
        availablePhases.Add(PhasesType.Mirror);
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
        
        if (phaseInputIdx <= 0)
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
    }

    private void TickPhaseCoolDownTimer()
    {
        if (!isPhaseCountdownActive)
        {
            return;
        }
        
        phaseActiveTimeElapsed += Time.unscaledDeltaTime;
        if (phaseActiveTimeElapsed >= phaseActiveDuration)
        {
            isPhaseCountdownActive = false;
            
            DisablePhaseEffect(activePhase);
            ActivatePhase(PhasesType.Default);
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
                break;
            
            case PhasesType.Air:
                EnableAirEffect();
                break;
            
            case PhasesType.AstralPlane:
                EnableAstralEffect();
                break;
            
            case PhasesType.Mirror:
                EnableMirrorEffect();
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
        Time.fixedDeltaTime = 0.02f * Time.timeScale;;
    }

    private void DisableTimeShiftEffect()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
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
}
