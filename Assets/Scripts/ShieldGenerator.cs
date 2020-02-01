using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : Machine
{
    private Shield shield;
    public float maxRepairRate;

    private void Awake()
    {
        shield = FindObjectOfType<Shield>();
    }

    private void Update()
    {
        if (shield) shield.Repair(maxRepairRate * HP / maxHP * Time.deltaTime);
    }

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
}
