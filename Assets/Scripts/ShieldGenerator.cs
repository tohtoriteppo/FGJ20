using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldGenerator : Machine
{
    private Shield shield;
    public float maxRepairRate;

    public override void Start()
    {
        base.Start();
        shield = FindObjectOfType<Shield>();
    }

    protected override void UpdateState()
    {
    }

    private void Update()
    {
       if (shield) shield.Repair(maxRepairRate * HP / maxHP * Time.deltaTime);
    }
}
