using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Rotation direction as Euler angles.")]
    public Vector3 rotationDirection = new Vector3(0f, 100f, 0f);

    [Tooltip("Rotation speed multiplier.")]
    public float rotationSpeed = 1f;

    [Tooltip("Rotate in World Space instead of Local Space.")]
    public bool useWorldSpace = false;

    void Update()
    {
        // Calculate final rotation per frame
        Vector3 euler = rotationDirection * rotationSpeed * Time.deltaTime;

        // Apply rotation
        transform.Rotate(euler, useWorldSpace ? Space.World : Space.Self);
    }
}