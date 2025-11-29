using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPointType checkPointType;
    [SerializeField] private Transform spawnT;
    [SerializeField] private ParticleSystem CheckPointActivatedEfx;
    public Transform SpawnT => spawnT;
    
    public CheckPointType CheckPointType => checkPointType;

    public void PlayCheckPointActivatedEfx()
    {
        CheckPointActivatedEfx.Play();
    }
}
