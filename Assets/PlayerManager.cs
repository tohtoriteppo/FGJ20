using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float groundSpeed = 5f;
    [SerializeField] float swimMaxSpeed = 5f;
    [SerializeField] float swimAcceleration = 5f;
    [SerializeField] float jumpForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
