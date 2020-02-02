using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wall : Damageable
{

    public float brokenAlpha = 0.1f;
    
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
            Debug.LogError(name + " - " + room.name + ": This room is already registered to this wall1");
        }
        rooms.Add(room);
        if (rooms.Count > 2)
        {
            Debug.LogError(name + " - " + room.name + ": This wall is between more than two rooms!");
        }
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
        if (rooms.Count == 0) Debug.LogError(name + ": This wall has no adjacent rooms!");
        return rooms.Count < 2;
    }



    protected override void UpdateState()

    {
        bool newState = HP <= 0;
        if (newState != broken)
        {
            broken = true;
            foreach (Room room in rooms)
            {
                bool gravityOn = !IsOuterHull() && !broken;
                room.TraverseGravity(gravityOn);  // NOTE: Its unnecessary to set gravity for both rooms
            }
            UpdateCollider();
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        if (!sprite) return;
        Color color = sprite.color;
        if (state == WallState.None) color.a = 0;
        else if (broken) color.a = brokenAlpha;
        else color.a = Mathf.Max(HP / maxHP, brokenAlpha + 0.2f);
        sprite.color = color;
    }

    void UpdateCollider()
    {
        //GetComponent<Collider2D>().enabled = !broken;
        GetComponent<Collider2D>().isTrigger = broken;
    }
}
