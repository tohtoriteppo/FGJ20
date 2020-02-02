using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenGenerator : Machine
{
    public float minActiveHP;

    public bool HasOxygen()
    {
        return HP > minActiveHP;
    }

    protected override void UpdateState()
    {
    }
}
