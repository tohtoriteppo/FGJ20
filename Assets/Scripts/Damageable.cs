using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Damageable : MonoBehaviour
{
    public float maxHP;
    protected float HP;
    
    abstract public float Repair(float value);
    abstract public float Damage(float value);
}

