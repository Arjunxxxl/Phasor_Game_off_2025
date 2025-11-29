using UnityEngine;

public class PhasePickUp : MonoBehaviour
{
    [SerializeField] private PhasesType phaseType;

    public void OnPhasePickedUp(Player player)
    {
        ObjectPooler.Instance.SpawnFromPool(0, transform.position, Quaternion.identity);
        ObjectPooler.Instance.SpawnFromPool(0, transform.position, Quaternion.identity);
        player.playerEfxManager.PlayItemPickup();
        
        player.phaseManager.AddNewPhase(phaseType);
        gameObject.SetActive(false);
    }
}
