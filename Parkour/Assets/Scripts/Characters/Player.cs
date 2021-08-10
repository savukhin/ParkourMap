using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterController
{
    public MainCameraController mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndRotate();
        Jump();
    }

    public void Jump() {
        if (Input.GetKeyDown(KeyCode.Space))
            character.Jump();
    }

    public void MoveAndRotate() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 hor = mainCamera.transform.forward;
        hor.y = 0;
        Vector3 vert = mainCamera.transform.right;
        vert.y = 0;
        Vector3 dir = hor * v + vert * h;
        if (dir.magnitude > 1)
            dir = dir.normalized;
        if (dir.magnitude > 0.2) {
            Move(dir);
            character.isMoving = true;
        } else {
            character.isMoving = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && character.isRunning) {
            character.isRunning = false;
        } else if (Input.GetKeyDown(KeyCode.LeftShift) && !character.isRunning) {
            character.isRunning = true;
        }
    }
}
