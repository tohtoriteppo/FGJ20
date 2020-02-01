using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Damageable
{
    public float minActivationHP = 50;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

    override public float Repair(float value)
    {
        float oldHP = HP;
        HP = Mathf.Min(HP + value, maxHP);
        UpdateCollider();
        UpdateColor();
        return HP - oldHP; // Return amount repaired
    }

    override public float Damage(float value)
    {
        float oldHP = HP;
        HP = Mathf.Max(HP - value, 0);
        UpdateCollider();
        UpdateColor();
        return oldHP - HP; // Return amount damaged
    }

    private void UpdateCollider()
    {
        if (collider) collider.enabled = HP > minActivationHP;
    }

    private void UpdateColor()
    {
        if (spriteRenderer)
        {
            Color color = spriteRenderer.color;
            color.a = HP / maxHP;
            spriteRenderer.color = color;
        }
    }
}
