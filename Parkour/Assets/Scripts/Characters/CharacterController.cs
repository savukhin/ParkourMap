using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Character character;
    
    protected void Move(Vector3 dir) {
        transform.position += dir * character.moveSpeed * Time.deltaTime;
        character.Rotate(dir);
    }
}
