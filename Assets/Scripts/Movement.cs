using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool grounded = true;

    private BoxCollider2D collider;

    private PlayerManager manager;
    private GameObject floor;

    private Rigidbody2D rb;
    private bool jumpingThrough;

    private int playerLayer = 9;
    private int platformLayer = 11;
    private bool onPlatform;

    private float oxygenLevel = 1;
    private float oxygenDecrease;

    private string jumpString;

    private bool dead = false;

    public GameObject oxygenBar;

    private List<GameObject> closeObjects;
    private GameObject platform;

    float mapX = 1f;
    float mapY = 1f;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        closeObjects = new List<GameObject>();
        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        Debug.Log("horz: "+ horzExtent);
        Debug.Log("vert: "+ vertExtent);
        // Calculations assume map is position at the origin
        minX = horzExtent - mapX / 2.0f;
        maxX = mapX / 2.0f - horzExtent;
        minY = vertExtent - mapY / 2.0f;
        maxY = mapY / 2.0f - vertExtent;

        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<PlayerManager>();
        jumpForce = manager.GetJumpForce();
        groundSpeed = manager.GetPlayerSpeed();
        swimMaxSpeed = manager.GetSwimMaxSpeed();
        swimAcceleration = manager.GetSwimAcceleration();
        oxygenDecrease = manager.GetOxygenDecrease();
        jumpString = "_button_" + manager.GetJumpButton();
        collider = GetComponent<BoxCollider2D>();
        oxygenBar = Instantiate(oxygenBar, FindObjectOfType<Canvas>().transform);
        oxygenBar.SetActive(false);
        SetGravity(true);
    }

    // Update is called once per frame
   
    
    void Update()
    {
        if(!dead)
        {
            TestGravity();
            Jump();
            DecreaseOxygen();
        }
        SetSpeeds();

    }

    

    private void FixedUpdate()
    {
        PlatformIgnoring();
        if(!dead) Move();
        Animate();
        RestrictBounds();
    }
    private void DecreaseOxygen()
    {
        if(!isGravity)
        {
            oxygenLevel -= oxygenDecrease;
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            oxygenBar.transform.position = new Vector2(pos.x, pos.y + 20);
            oxygenBar.GetComponent<Slider>().value = oxygenLevel;
            if (oxygenLevel < 0) Die();
        }
    }
    private void Die()
    {
        //die animations etc
        oxygenBar.SetActive(false);
        dead = true;
    }
    public void Resurrect()
    {
        oxygenLevel = 1;
    }
    private void Jump()
    {
        //try jumping
        if (Input.GetButtonDown("p" + playerNum + jumpString))
        {
            if (grounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                grounded = false;
            }
        }
    }
    public int GetPlayerNum()
    {
        return playerNum;
    }

    private void RestoreCollider()
    {
        collider.enabled = true;
        onPlatform = false;
        platform.GetComponent<Collider2D>().enabled = true;
        Debug.Log("RESTORED!");
    }
    private void PlatformIgnoring()
    {
        if(isGravity)
        {
           // Debug.Log("Onplatform! "+onPlatform+ " grounded! " + grounded);
            if (onPlatform && vertical == -1)
            {
                //collider.enabled = false;
                platform.GetComponent<Collider2D>().enabled = false;
                Invoke("RestoreCollider", 0.3f*rb.gravityScale);
            }
            else if (!onPlatform)
            {
                bool ignore = rb.velocity.y > 0 || !isGravity;
                Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, ignore);
            }
            
        }
        else
        {
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        }
        
        
    }

    private void RestrictBounds()
    {
        if (Mathf.Abs(transform.position.x) > minX || Mathf.Abs(transform.position.y) > minY)
        {
            rb.AddForce(new Vector2(-transform.position.x, -transform.position.y));
        }
    }
    private void Move()
    {
        
        if (isGravity)
        {
            rb.velocity = new Vector2(horizontal * groundSpeed, rb.velocity.y);

        }
        else
        {
            rb.AddForce(new Vector2(horizontal * swimAcceleration, vertical * swimAcceleration));
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, swimMaxSpeed);
        }
        
        
    }
    public bool IsDead()
    {
        return dead;
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
            else if(horizontal >0) GetComponent<SpriteRenderer>().flipX = false;

        }
        
    }

    private void SetSpeeds()
    {
        horizontal = Input.GetAxis("p" + playerNum + "_joystick_horizontal");
        vertical = Input.GetAxis("p" + playerNum + "_joystick_vertical");
    }

    private void TestGravity()
    {
        if(Input.GetButtonDown("p" + playerNum + "_button_y"))
        {
            SetGravity(!isGravity);
        }
    }
    public void SetGravity(bool on)
    {
        StartCoroutine( SetGrav(on));
    }
    IEnumerator SetGrav(bool on)
    {
        yield return new WaitForSeconds(0.3f);
        isGravity = on;
        rb.gravityScale = isGravity ? 1 : 0;
        if (isGravity)
        {
            oxygenLevel = 1;
            oxygenBar.SetActive(false);
            rb.freezeRotation = false;
        }
        else
        {
            oxygenBar.SetActive(true);
            rb.freezeRotation = true;
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == platformLayer)
        {
            onPlatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == platformLayer)
        {
            onPlatform = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5)
            {
                grounded = true;
                //Debug.Log("name: " + collision.gameObject.name);
                if (collision.gameObject.layer == platformLayer)
                {
                    onPlatform = true;
                    platform = collision.gameObject;
                }
            }
            else
            {
                grounded = false;
            }
        }
    }
}
