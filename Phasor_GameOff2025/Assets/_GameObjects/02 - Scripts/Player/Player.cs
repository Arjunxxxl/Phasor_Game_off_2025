using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Ref Self
    public PlayerMovement playerMovement { get; private set; }
    public PlayerAnimator playerAnimator { get; private set; }
    public PlayerCollisionDetection playerCollisionDetection { get; private set; }
    
    // Ref Others
    public PhaseManager phaseManager { get; private set; }
    public Inventory inventory { get; private set; }

    #region Singleton

    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
    }

    #region SetUp

    private void SetUp()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerCollisionDetection = GetComponent<PlayerCollisionDetection>();
        
        phaseManager = PhaseManager.Instance;
        inventory = Inventory.Instance;
        
        playerMovement.SetUp(this);
        playerAnimator.SetUp();
        playerCollisionDetection.SetUp(this);
        
        phaseManager.SetUp(this);

        Vector3 spawnPos = CheckPointManager.Instance.GetLastCheckPointSpawnPos();
        transform.position = spawnPos;
    }

    #endregion
}
