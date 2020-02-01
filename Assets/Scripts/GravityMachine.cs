using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMachine : Machine
{
    public float minActivationHP = 0;
    public GameObject healthBar;

    void Start()
    {
        healthBar = Instantiate(healthBar, FindObjectOfType<Canvas>().transform);
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        healthBar.transform.position = new Vector2(pos.x, pos.y + 40);
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

    public bool HasGravity()
    {
        return HP > minActivationHP;
    }
    
}
