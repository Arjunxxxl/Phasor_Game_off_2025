using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Models")] 
    [SerializeField] private GameObject containerGo;
    
    [Header("Movement Data")] 
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float maxJumpHeight_AirPhase;
    [SerializeField] private float maxJumpTime_AirPhase;
    [SerializeField] private float rotationSpeed;
    
    // Input
    private Vector2 moveInput;
    private bool jumpInput;
    
    // Movement data
    private float moveSpeed;
    private Vector3 moveDir;
    private float ySpeed;
    private Vector3 obstacleHitVelocity;
    private bool stopAllMovement;
    
    // Jump Data
    private float jumpSpeed;
    private float jumpSpeed_AirPhase;
    private bool isJumping = false;
    private float fallMul = 2.0f;
    
    // Grounded Data
    private bool isGrounded;
    
    // Gravity Data
    private float groundedGravity = -0.5f;
    private float jumpGravity;
    private float jumpGravity_AirPhase;
    private float gravity;

    private bool UseHighJump = false;
    
    // Character Controller
    private CharacterController characterController;
    
    // Player
    private Player player;
    private UserInput userInput;
    
    // Update is called once per frame
    void Update()
    {
        bool isDead = player.GetIsDead();
        
        if (isDead || stopAllMovement)
        {
            return;
        }
        
        GetInput();

        DetectGround();
        TriggerJump();
        MovePlayer();
        UpdateObstacleHitSpeed();

        RotatePlayer();
    }

    #region SetUp

    public void SetUp(Player player)
    {
        this.player = player;

        userInput = UserInput.Instance;
        
        characterController = GetComponent<CharacterController>();
        
        moveSpeed = baseMoveSpeed;
        gravity = groundedGravity;

        CalcJumpData();
    }

    #endregion

    #region Reset

    public void ResetAllMovement()
    {
        moveDir = Vector3.zero;
        ySpeed = 0.0f;

        isJumping = false;
        
        obstacleHitVelocity = Vector3.zero;
    }

    #endregion
    
    #region Input

    private void GetInput()
    {
        moveInput = userInput.MoveInput;
        jumpInput = userInput.JumpInput;
        
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            player.playerAnimator.PlayRunAnimation();
        }
        else
        {
            player.playerAnimator.StopRunAnimation();
        }
    }

    #endregion

    #region Movement

    private void MovePlayer()
    {
        moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveDir *= moveSpeed;

        UpdatedYSpeed();
        moveDir.y = ySpeed;

        moveDir += obstacleHitVelocity;
        
        characterController.Move(moveDir * Time.unscaledDeltaTime);
    }

    private void UpdatedYSpeed()
    {
        bool isFalling = ySpeed < 0 || !jumpInput;
        if (isGrounded)
        {
            gravity = groundedGravity;
            ySpeed += Time.unscaledDeltaTime * gravity;
        }
        else if (isFalling)
        {
            gravity = UseHighJump ? jumpGravity_AirPhase : jumpGravity;

            float prefSpeed = ySpeed;
            float nextSpeed = prefSpeed + Time.unscaledDeltaTime * gravity * fallMul;
            
            ySpeed = (prefSpeed + nextSpeed) * 0.5f;

            if (!isJumping)
            {
                player.playerAnimator.PlayFallAnimation();
            }
        }
        else
        {
            gravity = UseHighJump ? jumpGravity_AirPhase : jumpGravity;

            float prefSpeed = ySpeed;
            float nextSpeed = prefSpeed + Time.unscaledDeltaTime * gravity;
            
            ySpeed = (prefSpeed + nextSpeed) * 0.5f;
        }

        if (ySpeed <= -25.0f)
        {
            ySpeed = -25.0f;
        }
    }

    public void SetHighJump(bool useHighJump)
    {
        UseHighJump = useHighJump;
    }
    
    #endregion

    #region Jumping

    private void CalcJumpData()
    {
        float timeToApex = maxJumpTime * 0.5f;
        jumpGravity = -1 * (2 * maxJumpHeight / Mathf.Pow(timeToApex, 2));
        jumpSpeed = ((maxJumpHeight - (0.5f * jumpGravity * Mathf.Pow(timeToApex, 2.0f))) / timeToApex);
        
        float timeToApex_AirPhase = maxJumpTime_AirPhase * 0.5f;
        jumpGravity_AirPhase = -1 * (2 * maxJumpHeight_AirPhase / Mathf.Pow(timeToApex_AirPhase, 2));
        jumpSpeed_AirPhase = (maxJumpHeight_AirPhase - 
                              (0.5f * jumpGravity_AirPhase * Mathf.Pow(timeToApex_AirPhase, 2.0f))) / timeToApex_AirPhase;
    }
    
    private void TriggerJump()
    {
        if (!isJumping && isGrounded && jumpInput)
        {
            float speed = UseHighJump ? jumpSpeed_AirPhase : jumpSpeed;
            ySpeed = speed * 0.5f;
            isJumping = true;
            
            player.playerAnimator.PlayJumpAnimation();
        }
        else if (isGrounded)
        {
            isJumping = false;
            
            player.playerAnimator.StopJumpAnimation();
            player.playerAnimator.StopFallAnimation();
        }
    }

    #endregion

    #region Ground Detection

    private void DetectGround()
    {
        isGrounded = characterController.isGrounded;
    }

    #endregion

    #region Player Rotation

    private void RotatePlayer()
    {
        Vector3 rotationDir = moveDir;
        rotationDir.y = 0;

        if (rotationDir.x == 0 && rotationDir.z == 0)
        {
            return;
        }

        Quaternion newRotation = Quaternion.LookRotation(rotationDir, Vector3.up);

        containerGo.transform.rotation =
            Quaternion.Lerp(containerGo.transform.rotation, newRotation, rotationSpeed * Time.unscaledDeltaTime);
    }

    #endregion

    #region Movement Stopping

    public void StopAllMovement(bool stop)
    {
        stopAllMovement = stop;
    }

    #endregion
    
    #region Obstacle

    public void AddVelocityOnObstacle(Vector3 velocity)
    {
        obstacleHitVelocity = velocity;
    }

    private void UpdateObstacleHitSpeed()
    {
        float dt = Time.unscaledDeltaTime;

        obstacleHitVelocity.x = MoveTowardsZero(obstacleHitVelocity.x, Constants.Player.ObstacleHitSpeedDecrement.x * dt);
        obstacleHitVelocity.y = MoveTowardsZero(obstacleHitVelocity.y, Constants.Player.ObstacleHitSpeedDecrement.y * dt);
        obstacleHitVelocity.z = MoveTowardsZero(obstacleHitVelocity.z, Constants.Player.ObstacleHitSpeedDecrement.z * dt);
    } 
    
    private float MoveTowardsZero(float value, float dec)
    {
        if (value > 0)
        {
            value -= dec;
            
            if (value < 0)
            {
                value = 0;
            }
        }
        else if (value < 0)
        {
            value += dec;
            
            if (value > 0)
            {
                value = 0;
            }
        }
        return value;
    }

    #endregion

    #region Getters

    public Vector3 GetActualForwardDir()
    {
        return containerGo.transform.forward;
    }

    #endregion
}
