using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float groundSpeed;
    [SerializeField] float swimMaxSpeed;
    [SerializeField] float swimAcceleration;
    [SerializeField] float jumpForce;
    [SerializeField] float throwForce;
    [SerializeField] float throwChargeTime;
    [SerializeField] float repairAmount;
    [SerializeField] float oxygenDecrease;

    private int wallLayer = 8;
    private int playerLayer = 9;
    private int passableLayer = 10;
    private int platformLayer = 11;

    private Movement[] movements;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(passableLayer, playerLayer);
        movements = FindObjectsOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckLose()) Lose();
    }

    private void Lose()
    {

    }

    private bool CheckLose()
    {
        bool allDead = true;
        foreach (Movement player in movements)
        {
            if (!player.IsDead()) allDead = false;
        }
        return allDead;
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
    public float GetRepairAmount()
    {
        return throwChargeTime;
    }
    public float GetOxygenDecrease()
    {
        return oxygenDecrease;
    }
}
