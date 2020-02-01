using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float swimMaxSpeed = 5f;
    [SerializeField] float swimAcceleration = 5f;
    [SerializeField] float jumpForce = 500f;
    [SerializeField] float throwForce = 10f;
    [SerializeField] float throwChargeTime = 2f;

    private int wallLayer = 8;
    private int playerLayer = 9;
    private int passableLayer = 10;
    private int platformLayer = 11;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(passableLayer, playerLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetPlayerSpeed()
    {
        return groundSpeed;
    }
    public float GetJumpForce()
    {
        return jumpForce;
    }
    public float GetSwimMaxSpeed()
    {
        return swimMaxSpeed;
    }
    public float GetSwimAcceleration()
    {
        return swimAcceleration;
    }
    public float GetThrowForce()
    {
        return throwForce;
    }
    public float GetThrowChargeTime()
    {
        return throwChargeTime;
    }
}
