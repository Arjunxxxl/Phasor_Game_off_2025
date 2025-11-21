using UnityEngine;

public class ObstacleRotatingHammer : MonoBehaviour
{
    [SerializeField] private bool isFast;
    [SerializeField] private Vector3 rotationAxis;
    private float rotationSpeed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = isFast ? 
            Constants.ObstacleData.RotatingHammerSpeed_Fast : Constants.ObstacleData.RotatingHammerSpeed_Normal;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
    }

    public Vector3 GetPerpendicular()
    {
        return transform.forward * -1;
    }
}
