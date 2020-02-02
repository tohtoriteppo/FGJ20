using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityMachine : Machine
{
    public float minActivationHP = 0;
    public GameObject healthBar;

    void Start()
    {
        healthBar = Instantiate(healthBar, FindObjectOfType<Canvas>().transform);
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        healthBar.transform.position = new Vector2(pos.x, pos.y + 40);
        healthBar.GetComponent<Slider>().value = HP;
    }

    override public float Repair(float value)
    {
        float oldHP = HP;
        HP = Mathf.Min(HP + value * (1 - ((HP-1) / (maxHP))), maxHP);
        healthBar.GetComponent<Slider>().value = HP;
        return HP - oldHP; // Return amount repaired
    }

    override public float Damage(float value)
    {
        float oldHP = HP;
        HP = Mathf.Max(HP - value, 0);
        healthBar.GetComponent<Slider>().value = HP;
        return oldHP - HP; // Return amount damaged
    }

    public bool HasGravity()
    {
        return HP > minActivationHP;
    }
    
}
