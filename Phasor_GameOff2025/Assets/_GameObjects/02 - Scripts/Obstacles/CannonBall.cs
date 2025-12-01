using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Vector2 shootSpeedRange;
    
    private Rigidbody rigidbody;

    public void SetUp(Vector3 moveDirection)
    {
        if (rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        
        float shootSpeed = Random.Range(shootSpeedRange.x, shootSpeedRange.y);
        rigidbody.AddForce(moveDirection * shootSpeed,  ForceMode.VelocityChange);

        rigidbody.AddTorque(moveDirection * shootSpeed);

        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(15.0f);
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
