using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteroScript : MonoBehaviour
{

    float baseHealth = 100.0f;

    public float currentHealth = 100.0f;

    public Vector2 directionVector;
    public float speed = 1.0f;

    public float baseSpeed = 1.0f;
    public float minSpeed = 0.2f;

    int flag = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    float H2S()
    {
        return (baseSpeed - minSpeed) * currentHealth / baseHealth + minSpeed ;
    }

    void ded()
    {
        Destroy(gameObject);
        //Spawn particles
        //Play sounds
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = H2S();
        GetComponent<Rigidbody2D>().velocity = directionVector*speed;
        GameObject other = collision.collider.gameObject;
        //other.GetComponent<BoxCollider2D>().enabled = false;
        if (other.GetComponent<Damageable>())
        {
            currentHealth -= other.GetComponent<Damageable>().Damage(currentHealth);
            if (currentHealth <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<ParticleSystem>().Stop();
                Invoke("ded", 2);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        directionVector = GetComponent<Rigidbody2D>().velocity.normalized;
        speed = GetComponent<Rigidbody2D>().velocity.magnitude;

        if (speed < minSpeed/2 && flag++ == 0)
        {
            Invoke("ded", 2);
        }
    }
}
