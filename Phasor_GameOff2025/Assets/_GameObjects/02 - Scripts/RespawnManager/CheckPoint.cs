using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private CheckPointType checkPointType;
    [SerializeField] private Transform spawnT;
    public Transform SpawnT => spawnT;
    
    public CheckPointType CheckPointType => checkPointType;
}
