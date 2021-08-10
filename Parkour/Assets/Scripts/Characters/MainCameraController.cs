using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public GameObject target;
    Vector3 positionForCamera;
    private Vector3 startOffset;
    private float zoom = 1;
    private Rigidbody m_rigidbody;

    void Start() {
        startOffset = transform.position - target.transform.position;
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        zoom -= 0.1f * Input.mouseScrollDelta.y;
        zoom = Mathf.Max(zoom, 0.1f);
        zoom = Mathf.Min(zoom, 2);
    }

    void LateUpdate() {
        transform.position = target.transform.position;
        transform.Rotate(new Vector3(1, 0, 0), Input.GetAxis("Mouse Y"));
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Mouse X"), Space.World);
        transform.Translate(startOffset * zoom);
    }
}
