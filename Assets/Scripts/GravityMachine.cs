using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityMachine : Machine
{
    public float minActivationHP;

    protected override void UpdateState()
    {

    }

    public bool HasGravity()
    {
        return HP > minActivationHP;
    }
    
}
