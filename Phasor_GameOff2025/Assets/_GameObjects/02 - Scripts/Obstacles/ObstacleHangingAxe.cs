using UnityEngine;

public class ObstacleHangingAxe : MonoBehaviour
{
    [SerializeField] private bool isFast;
    [SerializeField] private Vector3 minRotationAngle;
    [SerializeField] private Vector3 maxRotationAngle;
    private float rotationSpeed;
    private Quaternion minRotation;
    private Quaternion maxRotation;
    
    private float fac = 0f;
    private float prevFac = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minRotation = Quaternion.Euler(minRotationAngle);
        maxRotation = Quaternion.Euler(maxRotationAngle);
        
        rotationSpeed = isFast ? 
            Constants.ObstacleData.HangingAxeRotationSpeed_Fast : Constants.ObstacleData.HangingAxeRotationSpeed_Normal;
    }

    // Update is called once per frame
    void Update()
    {
        prevFac = fac;
        
        fac = Mathf.PingPong(Time.time * rotationSpeed, 1.0f);
        transform.rotation = Quaternion.Lerp(minRotation, maxRotation, fac);
    }

    public bool IsMovingToRight => fac > prevFac;
}
