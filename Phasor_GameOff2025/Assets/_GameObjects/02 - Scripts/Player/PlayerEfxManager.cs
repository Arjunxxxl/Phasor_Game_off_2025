using UnityEngine;

public class PlayerEfxManager : MonoBehaviour
{
    [Header("Efx")]
    [SerializeField] private ParticleSystem phaseItemPickupEfx;
    
    public void PlayItemPickup()
    {
        phaseItemPickupEfx.Play();
    }
}
