using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Damageable
{

    public Color brokenColor;
    public Color solidColor;
    
    public enum WallState
    {
        Solid,
        Airlock,
        None
    }

    public bool broken = false;
    public WallState state;
    public List<Room> rooms = new List<Room>();
    private SpriteRenderer sprite;

    private void Awake()
    {
        HP = maxHP;
        sprite = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    public void AddRoom(Room room)
    {
        if (rooms.Contains(room))
        {
            Debug.LogError("This room is already registered to this wall1");
        }
        rooms.Add(room);
    }

    public bool CanPlayerGetThrough()
    {
        return broken || state == WallState.Airlock || state == WallState.None;
    }

    public bool CanGravityGetThrough()
    {
        return broken || state == WallState.None;
    }

    public bool IsOuterHull()
    {
        return rooms.Count < 2;
    }

    override public float Repair(float value)
    {
        float oldHP = HP;
        HP = Mathf.Min(HP + value, maxHP);
        if (broken && HP > 0)
        {
            broken = false;
            foreach (Room room in rooms)
            {
                room.SetGravity(true);
            }
            UpdateCollider();
            
        }    
        UpdateColor();
        return HP - oldHP; // Return amount repaired
    }

    override public float Damage(float value)
    {
        float oldHP = HP;
        HP = Mathf.Max(HP - value, 0);
        
        if (!broken && HP <= 0)
        {
            broken = true;
            foreach (Room room in rooms)
            {
                room.SetGravity(!IsOuterHull());  // TODO: Its unnecessary to set gravity for both rooms
                

            }
            UpdateCollider();
        }
        UpdateColor();
        return oldHP - HP; // Return amount damaged
    }

    void UpdateColor()
    {
        if (!sprite) return;
        if (broken) sprite.color = brokenColor;
        else
        {
            Color color = solidColor;
            color.a = Mathf.Max(HP / maxHP, brokenColor.a + 0.2f);
            sprite.color = color;
            Debug.Log(color);
        }
        
    }

    void UpdateCollider()
    {
        GetComponent<Collider2D>().enabled = !broken;
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
