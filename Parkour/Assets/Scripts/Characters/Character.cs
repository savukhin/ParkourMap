using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public class Events {
        UnityEngine.Events.UnityEvent changeHP;
    }

    public Events events;
    public float moveSpeed = 2;
    public float runSpeed = 3;
    public float jumpForce = 4;

    public bool isRunning {
        get {
            currentSpeed = moveSpeed;
            return m_animator.GetBool("Run");
        }
        set {
            currentSpeed = runSpeed;
            m_animator.SetBool("Run", value);
        }
    }
    public bool isMoving {
        get {
            return m_animator.GetBool("Move");
        }
        set {
            m_animator.SetBool("Move", value);
        }
    }

    private Rigidbody m_rigidbody;
    private Animator m_animator;
    private float currentSpeed;
    private bool blockMove = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        currentSpeed = moveSpeed;
    }

    private bool isGrounded(float extraDimension=1f) {
        return Physics.BoxCast(GetComponent<Collider>().bounds.center, 
                                GetComponent<Collider>().bounds.extents - Vector3.one * extraDimension,
                                new Vector3(1f, -1f, 1f), transform.rotation, 2 * extraDimension);
    }

    IEnumerator JumpProcessing() {
        blockMove = true;

        m_animator.SetTrigger("Jump");
        yield return null;

        AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);

        m_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        while (m_rigidbody.velocity.y >= 0) {
            yield return null;
        }
        blockMove = false;
        
        m_animator.SetBool("Falling", true);
        while (!isGrounded())
            yield return null;

        m_animator.SetBool("Falling", false);
        m_animator.SetTrigger("Grounded");
        yield return null;
    }

    public void Jump() {
        if (!isGrounded() || blockMove)
            return;
        StartCoroutine("JumpProcessing");
    }
    
    public void Move(Vector3 dir) {
        if (blockMove)
            return;
        transform.position += dir * currentSpeed * Time.deltaTime;
        Quaternion finalRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, finalRotation, 360 * Time.deltaTime);
    }
}
