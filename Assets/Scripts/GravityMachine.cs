﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMachine : Machine
{
    public float minActivationHP = 50;
    
    override public float Repair(float value)
    {
        float oldHP = HP;
        HP = Mathf.Min(HP + value, maxHP);
        return HP - oldHP; // Return amount repaired
    }

    override public float Damage(float value)
    {
        float oldHP = HP;
        HP = Mathf.Max(HP - value, 0);
        return oldHP - HP; // Return amount damaged
    }

    public bool HasGravity()
    {
        return HP > minActivationHP;
    }
    
}
