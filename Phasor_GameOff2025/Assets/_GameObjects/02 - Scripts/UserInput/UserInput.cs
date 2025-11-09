using UnityEngine;

public class UserInput : MonoBehaviour
{
    private Vector2 moveInput;
    private bool jumpInput;
    
    public Vector2 MoveInput => moveInput;
    public bool JumpInput => jumpInput;
    
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
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();
        
        jumpInput = Input.GetButton("Jump");
    }
}
