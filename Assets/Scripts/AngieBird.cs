using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AngieBird : MonoBehaviour
{
    private Rigidbody2D rigidBody; 
    private CircleCollider2D circleCollider;
    private bool hasBeenLaunched = false;
    private bool shouldFaceVelDirection = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        
    }

    public void Start()
    {
        rigidBody.isKinematic = true;
        circleCollider.enabled = false;    
    }

    //if you want to change or tweak "physics" used this method
    private void FixedUpdate() 
    {
        //prevents fighting with changing transform.right in slingshot handler
        if(hasBeenLaunched && shouldFaceVelDirection)
        {
            transform.right = rigidBody.velocity;
        }
        
    }
    public void LaunchBird(Vector2 direction, float force)
    {
        rigidBody.isKinematic = false;
        circleCollider.enabled = true;

        //apply force
        rigidBody.AddForce(direction * force, ForceMode2D.Impulse);
        
        hasBeenLaunched = true;
        shouldFaceVelDirection = true;

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        shouldFaceVelDirection = false; 
        
    }
    
}
