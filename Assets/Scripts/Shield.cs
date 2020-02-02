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

    protected override void UpdateState()
    {
        UpdateCollider();
        UpdateColor();
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
