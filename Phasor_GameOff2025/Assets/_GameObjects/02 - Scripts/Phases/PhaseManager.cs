using System;
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

    // ref
    private Player player;
    private UserInput userInput;
    
    // Properties
    public PhasesType ActivePhase => activePhase;

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

        ActivatePhase(PhasesType.Default);
    }

    private void LoadAvailablePhases()
    {
        availablePhases = new List<PhasesType>();
        availablePhases.Add(PhasesType.Default);
        availablePhases.Add(PhasesType.TimeShift);
        availablePhases.Add(PhasesType.Air);
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

    #endregion
}
