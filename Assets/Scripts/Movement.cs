using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] int playerNum = 1;
    [SerializeField] float maxSpeed = 5f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("p" + playerNum + "_joystick_horizontal");
        float vertical = Input.GetAxis("p" + playerNum + "_joystick_vertical");
        Move(horizontal, vertical);
    }

    private void Move(float horizontal, float vertical)
    {

        rb.AddForce(new Vector2(horizontal, vertical));
        //cap to max speed
        Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
