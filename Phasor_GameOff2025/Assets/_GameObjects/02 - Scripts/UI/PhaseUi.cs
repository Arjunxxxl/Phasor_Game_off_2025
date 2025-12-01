using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUi : MonoBehaviour
{
    [Header("Phase Ui")]
    [SerializeField] private GameObject timePhaseUi;
    [SerializeField] private GameObject airPhaseUi;

    [Header("In active panels")]
    [SerializeField] private Image timePhaseInActiveImg;
    [SerializeField] private Image airPhaseInActiveImg;

    #region SetUp

    public void SetUp(List<PhasesType> availablePhases)
    {
        timePhaseUi.SetActive(availablePhases.Contains(PhasesType.TimeShift));
        airPhaseUi.SetActive(availablePhases.Contains(PhasesType.Air));

        timePhaseInActiveImg.fillAmount = 0.0f;
        airPhaseInActiveImg.fillAmount = 0.0f;
    }
    
    public void UpdatePhaseUi(List<PhasesType> availablePhases)
    {
        timePhaseUi.SetActive(availablePhases.Contains(PhasesType.TimeShift));
        airPhaseUi.SetActive(availablePhases.Contains(PhasesType.Air));
    }

    #endregion

    #region In active ui

    public void UpdateInActiveUi(float timeElapsed, float maxDuration)
    {
        float fillAmt = (maxDuration - timeElapsed) /  maxDuration;
        
        timePhaseInActiveImg.fillAmount = fillAmt;
        airPhaseInActiveImg.fillAmount = fillAmt;
    }

    public void RemoveInActiveUi()
    {
        timePhaseInActiveImg.fillAmount = 0.0f;
        airPhaseInActiveImg.fillAmount = 0.0f;
    }

    #endregion
}
