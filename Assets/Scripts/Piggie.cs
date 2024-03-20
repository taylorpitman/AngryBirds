using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Piggie : MonoBehaviour
{
    [SerializeField] private const float MAX_HEALTH= 3f;
    [SerializeField] private float damageThreshhold = 0.02f;
    [SerializeField] private GameObject deathParticle;
    private float currentHealth;

    private void Awake() 
    {
        currentHealth = MAX_HEALTH;
    }
    
    public void DamagePiggie(float damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.gameManager.RemovePiggie(this);

        Instantiate(deathParticle, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if(impactVelocity > damageThreshhold)
        {
            DamagePiggie(impactVelocity);
        }
    }
}
