using UnityEngine;

public class PlayerEfxManager : MonoBehaviour
{
    [Header("Efx")]
    [SerializeField] private ParticleSystem phaseItemPickupEfx;
    [SerializeField] private ParticleSystem landEfx;
    [SerializeField] private ParticleSystem jumpfx;
    [SerializeField] private ParticleSystem hitfx;
    
    public void PlayItemPickup()
    {
        phaseItemPickupEfx.Play();
    }
    
    public void PlayLandEfx()
    {
        if (landEfx.isPlaying)
        {
            landEfx.Stop();
        }
        
        landEfx.Play();
    }
    
    public void PlayJumpEfx()
    {
        jumpfx.Play();
    }
    
    public void StopJumpEfx()
    {
        jumpfx.Stop();
    }
    
    public void PlayHitEfx()
    {
        hitfx.Play();
    }
}
