using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Rig")] 
    [SerializeField] private Transform rigT;
    [SerializeField] private Vector3 rigOffset;
    [SerializeField] private Vector3 rigRotation;
    [SerializeField] private float rigMoveSpeed;

    [Header("Pivot")] 
    [SerializeField] private Transform pivotT;
    [SerializeField] private Vector3 pivotOffset;
    [SerializeField] private Vector3 pivotRotation;
    
    // Target
    private Transform targetT;
    
    #region Singleton

    public static CameraController Instance;

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
    
    private void Start()
    {
        targetT = Player.Instance.transform;

        SetUpRig();
        SetUpPivot();
    }

    private void LateUpdate()
    {
        MoveRigToTarget();
    }

    #region Restting

    public void SnapCameraToPlayer()
    {
        SetUpRig();
        SetUpPivot();
    }

    #endregion
    
    #region Rig

    private void SetUpRig()
    {
        Vector3 rigPos = targetT.position + rigOffset;
        rigT.position = rigPos;
        
        rigT.rotation = Quaternion.Euler(rigRotation);
    }

    private void MoveRigToTarget()
    {
        Vector3 targetPos = targetT.position + rigOffset;
        rigT.transform.position = Vector3.MoveTowards(rigT.transform.position,
            targetPos,
            rigMoveSpeed * Time.unscaledDeltaTime);
    }

    #endregion

    #region Pivot

    private void SetUpPivot()
    {
        pivotT.transform.localPosition = pivotOffset;
        pivotT.localRotation = Quaternion.Euler(pivotRotation); 
    }

    #endregion
}
