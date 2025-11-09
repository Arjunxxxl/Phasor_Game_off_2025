using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Models")] 
    [SerializeField] private GameObject containerGo;
    
    [Header("Movement Data")] 
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float rotationSpeed;
    
    // Input
    private Vector2 moveInput;
    private bool jumpInput;
    
    // Movement data
    private float moveSpeed;
    private Vector3 moveDir;
    private float ySpeed;
    
    // Jump Data
    private float jumpSpeed;
    private bool isJumping = false;
    private float fallMul = 2.0f;
    
    // Grounded Data
    private bool isGrounded;
    
    // Gravity Data
    private float groundedGravity = -0.5f;
    private float jumpGravity;
    private float gravity;
    
    // Character Controller
    private CharacterController characterController;
    
    // Player
    private Player player;
    private UserInput userInput;
    
    // Update is called once per frame
    void Update()
    {
        GetInput();

        DetectGround();
        TriggerJump();
        MovePlayer();

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

        float timeToApex = maxJumpTime * 0.5f;
        jumpGravity = -1 * (2 * maxJumpHeight / Mathf.Pow(timeToApex, 2));
        jumpSpeed = ((maxJumpHeight - (0.5f * jumpGravity * Mathf.Pow(timeToApex, 2.0f))) / timeToApex);
    }

    #endregion

    #region Input

    private void GetInput()
    {
        moveInput = userInput.MoveInput;
        jumpInput = userInput.JumpInput;
    }

    #endregion

    #region Movement

    private void MovePlayer()
    {
        moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        moveDir *= moveSpeed;

        UpdatedYSpeed();
        moveDir.y = ySpeed;
        
        characterController.Move(moveDir * Time.deltaTime);

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            player.playerAnimator.PlayRunAnimation();
        }
        else
        {
            player.playerAnimator.StopRunAnimation();
        }
    }

    private void UpdatedYSpeed()
    {
        bool isFalling = ySpeed < 0 || !jumpInput;
        if (isGrounded)
        {
            gravity = groundedGravity;
            ySpeed += Time.deltaTime * gravity;
        }
        else if (isFalling)
        {
            gravity = jumpGravity;

            float prefSpeed = ySpeed;
            float nextSpeed = prefSpeed + Time.deltaTime * gravity * fallMul;
            
            ySpeed = (prefSpeed + nextSpeed) * 0.5f;

            if (!isJumping)
            {
                player.playerAnimator.PlayFallAnimation();
            }
        }
        else
        {
            gravity = jumpGravity;

            float prefSpeed = ySpeed;
            float nextSpeed = prefSpeed + Time.deltaTime * gravity;
            
            ySpeed = (prefSpeed + nextSpeed) * 0.5f;
        }

        if (ySpeed <= -25.0f)
        {
            ySpeed = -25.0f;
        }
    }
    
    #endregion

    #region Jumping

    private void TriggerJump()
    {
        if (!isJumping && isGrounded && jumpInput)
        {
            ySpeed = jumpSpeed * 0.5f;
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

        Quaternion newRotation = Quaternion.LookRotation(rotationDir, Vector3.up);

        containerGo.transform.rotation =
            Quaternion.Lerp(containerGo.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

    #endregion
}
