using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] int playerNum = 1;
    private float groundSpeed;
    private float jumpForce;
    private float swimMaxSpeed;
    private float swimAcceleration;

    private float horizontal;
    private float vertical;

    private bool isGravity = false;
    private bool jumped = false;

    private PlayerManager manager;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<PlayerManager>();
        jumpForce = manager.GetJumpForce();
        groundSpeed = manager.GetPlayerSpeed();
        swimMaxSpeed = manager.GetSwimMaxSpeed();
        swimAcceleration = manager.GetSwimAcceleration();
    }

    // Update is called once per frame
   
    
    void Update()
    {
        Repair();
        SetSpeeds();
    }


    private void FixedUpdate()
    {
        Move();
        Animate();
    }

    private void Move()
    {
        
        if (isGravity)
        {
            rb.velocity = new Vector2(horizontal*groundSpeed, rb.velocity.y);
            if (!jumped && vertical == 1)
            {
                rb.velocity = new Vector2(rb.velocity.x, vertical * jumpForce);
                jumped = true;
            }

            //cap to max speed
            //rb.velocity = new Vector2(Mathf.Min(rb.velocity.x, maxSpeed), rb.velocity.y);
        }
        else
        {
            rb.AddForce(new Vector2(horizontal * swimAcceleration, vertical * swimAcceleration));
            Debug.Log("Swimspeed " + swimMaxSpeed);
            Debug.Log("velocity1 " + rb.velocity.magnitude);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, swimMaxSpeed);
            Debug.Log("velocity2 " + rb.velocity.magnitude);

        }
    }

    private void Animate()
    {
        if(!isGravity)
        {
            //rotate towards velo
            //Vector2 v = rb.velocity;
            //rotate towards inputs
            if(horizontal != 0 || vertical != 0)
            {
                Vector2 v = new Vector2(horizontal, vertical);
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.Rotate(new Vector3(0, 0, 1), -90);
            }
            
        }
        else
        {
            transform.rotation = Quaternion.identity;
            if(horizontal < 0) GetComponent<SpriteRenderer>().flipX = true;
            else GetComponent<SpriteRenderer>().flipX = false;

        }
        
    }
    private void SetSpeeds()
    {
        horizontal = Input.GetAxis("p" + playerNum + "_joystick_horizontal");
        vertical = Input.GetAxis("p" + playerNum + "_joystick_vertical");
    }

    private void Repair()
    {
        if(Input.GetButtonDown("p" + playerNum + "_button_x"))
        {
            SetGravity(!isGravity);
        }
    }
    public void SetGravity(bool on)
    {
        isGravity = on;
        rb.gravityScale = isGravity ? 1 : 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
            {
                jumped = false;
            }
        }
        //check if collision below?
    }
}
