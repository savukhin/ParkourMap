using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Character character;
    
    protected void Move(Vector3 dir) {
        character.Move(dir);
    }
}
