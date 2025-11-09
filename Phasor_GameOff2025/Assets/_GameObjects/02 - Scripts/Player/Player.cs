using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Ref
    public PlayerMovement playerMovement { get; private set; }
    public PlayerAnimator playerAnimator { get; private set; }

    #region

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
        
        playerMovement.SetUp(this);
        playerAnimator.SetUp();
    }

    #endregion
}
