using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AngieBird : MonoBehaviour
{
    private Rigidbody2D rigidBody; 
    private CircleCollider2D circleCollider;

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
    public void LaunchBird(Vector2 direction, float force)
    {
        rigidBody.isKinematic = false;
        circleCollider.enabled = true;

        //apply force
        rigidBody.AddForce(direction * force, ForceMode2D.Impulse);

    }
    
}
