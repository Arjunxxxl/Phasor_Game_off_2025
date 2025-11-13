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
            return;
        }
        
        if (!availablePhases.Contains(phase))
        {
            return;
        }
        
        activePhase = phase;
        
        phaseActiveTimeElapsed = 0.0f;
        phaseActiveDuration = Constants.PhaseData.PhaseActiveDuration;
        isPhaseCountdownActive = activePhase != PhasesType.Default;
    }

    private void TickPhaseCoolDownTimer()
    {
        if (!isPhaseCountdownActive)
        {
            return;
        }
        
        phaseActiveTimeElapsed += Time.deltaTime;
        if (phaseActiveTimeElapsed >= phaseActiveDuration)
        {
            isPhaseCountdownActive = false;
            ActivatePhase(PhasesType.Default);
        }
    }

    #endregion
}
