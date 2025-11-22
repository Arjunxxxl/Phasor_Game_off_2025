using UnityEngine;

public class SimpleOscillator : MonoBehaviour
{
    [Header("Oscillation Settings")]
    public Vector3 pointA = Vector3.zero;
    public Vector3 pointB = new Vector3(0f, 1f, 0f);

    [Tooltip("Movement speed between the two points.")]
    public float speed = 1f;

    [Tooltip("Use World Space instead of Local Space.")]
    public bool useWorldSpace = false;

    private float t = 0f;
    private bool forward = true;

    void Update()
    {
        // Progress value 0→1
        t += (forward ? 1 : -1) * speed * Time.deltaTime;

        // Reverse direction at endpoints
        if (t >= 1f)
        {
            t = 1f;
            forward = false;
        }
        else if (t <= 0f)
        {
            t = 0f;
            forward = true;
        }

        // Apply smoothstep for ease-in / ease-out
        float smoothT = Mathf.SmoothStep(0f, 1f, t);

        // Calculate current position
        Vector3 newPos = Vector3.Lerp(pointA, pointB, smoothT);

        // Apply position in the chosen space
        if (useWorldSpace)
            transform.position = newPos;
        else
            transform.localPosition = newPos;
    }
}
