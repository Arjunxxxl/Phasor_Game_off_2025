using UnityEngine;

public class UserInput : MonoBehaviour
{
    // Player Movement Input
    private Vector2 moveInput;
    private bool jumpInput;

    // Phase Input
    private int phaseInput = -1;
    
    public Vector2 MoveInput => moveInput;
    public bool JumpInput => jumpInput;
    public int PhaseInput => phaseInput;
    
    #region

    public static UserInput Instance;

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
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager.Instance.PauseResumeGame(); 
        }
        
        GetPlayerMovementInput();
        GetPhasedInput();
    }

    private void GetPlayerMovementInput()
    {
        moveInput = Vector2.zero;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        
        jumpInput = Input.GetButton("Jump");
    }

    private void GetPhasedInput()
    {
        phaseInput = -1;
        
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            phaseInput = 0;
        } 
        
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.X))
        {
            phaseInput = 1;
        } 
        
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.C))
        {
            phaseInput = 2;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            phaseInput = 3;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            phaseInput = 4;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            phaseInput = 5;
        }
    }
}
