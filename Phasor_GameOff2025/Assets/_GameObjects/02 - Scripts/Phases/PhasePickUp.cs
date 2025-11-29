using UnityEngine;

public class PhasePickUp : MonoBehaviour
{
    [SerializeField] private PhasesType phaseType;

    public void OnPhasePickedUp(Player player)
    {
        player.phaseManager.AddNewPhase(phaseType);
        gameObject.SetActive(false);
    }
}
