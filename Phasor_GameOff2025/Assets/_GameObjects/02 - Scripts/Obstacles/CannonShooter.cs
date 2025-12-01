using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CannonShooter : MonoBehaviour
{
    [Header("Spawn Pos")] 
    [SerializeField] private Transform cannonBallSpawnT;
    [SerializeField] private Vector2 cannonSpawnGapRange;
    private float cannonSpawnGap;
    private float timeElapsed;
    
    // Ref
    private ObjectPooler objectPooler;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPooler = ObjectPooler.Instance;
        CalcNewSpawnTime();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= cannonSpawnGap)
        {
            SpawnNewCannonBall();
            CalcNewSpawnTime();
        }
    }

    private void CalcNewSpawnTime()
    {
        timeElapsed = 0;
        cannonSpawnGap = Random.Range(cannonSpawnGapRange.x, cannonSpawnGapRange.y);
    }

    private void SpawnNewCannonBall()
    {
        GameObject cannonBall = objectPooler.SpawnFromPool(3, cannonBallSpawnT.transform.position, Quaternion.identity);
        cannonBall.GetComponent<CannonBall>().SetUp(cannonBallSpawnT.transform.up);
    }
}
