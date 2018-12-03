using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour 
{
    public Rigidbody2D RigidBody;
    public float JumpVelocity;
    public float JumpDelay;

    private float TimeElapsed;
    
    void Update() 
    {
        TimeElapsed += Time.deltaTime;

        if (TimeElapsed > JumpDelay)
        {
            RigidBody.velocity = Vector2.up * JumpVelocity;
            TimeElapsed = 0;
        }
    }
}