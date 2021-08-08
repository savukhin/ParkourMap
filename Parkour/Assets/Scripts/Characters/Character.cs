using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public class Events {
        UnityEngine.Events.UnityEvent changeHP;
    }

    public Events events;
    public float moveSpeed = 2;
    public float jumpForce = 4;

    private Rigidbody m_rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private bool isGrounded(float extraDimension=1f) {
        return Physics.BoxCast(GetComponent<Collider>().bounds.center, 
                                GetComponent<Collider>().bounds.extents - Vector3.one * extraDimension,
                                new Vector3(1f, -1f, 1f), transform.rotation, 2 * extraDimension);
    }

    public void Jump() {
        if (!isGrounded())
            return;
        m_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    public void Rotate(Vector3 dir) {
        Quaternion finalRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 360 * Time.deltaTime);
    }

    public void Move(Vector3 dir) {
        // Quaternion finalRotation = Quaternion.
    }
}
