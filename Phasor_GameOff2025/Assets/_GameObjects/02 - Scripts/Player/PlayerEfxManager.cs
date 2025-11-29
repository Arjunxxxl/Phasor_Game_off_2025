using UnityEngine;

public class PlayerEfxManager : MonoBehaviour
{
    [Header("Efx")]
    [SerializeField] private ParticleSystem phaseItemPickupEfx;
    
    public void PlayPhaseItemPickup()
    {
        phaseItemPickupEfx.Play();
    }
}
