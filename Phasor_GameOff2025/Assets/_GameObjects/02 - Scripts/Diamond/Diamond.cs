using UnityEngine;

public class Diamond : MonoBehaviour
{
    [Header("Animation data")] 
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float amplitude;
    
    private Vector3 originalPos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float yOffset = Mathf.Sin(Random.Range(0.0f, Mathf.PI))* amplitude;
        transform.position += new Vector3(0, yOffset, 0);
        
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * moveSpeed) * amplitude;
        transform.position = originalPos + new Vector3(0, yOffset, 0);
        
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.Self);
    }
}
