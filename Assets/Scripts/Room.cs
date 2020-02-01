using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool test = false;
    public bool gravity = true;
    public bool roomChecked = false;
    public List<Wall> walls = new List<Wall>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.gameObject.GetComponent<Wall>())
        {
            Wall wall = other.gameObject.GetComponent<Wall>();
            if (walls.Contains(wall))
            {
                Debug.LogError("This room is already registered to this wall1");
            }
            walls.Add(wall);
            wall.AddRoom(this);
        }
    }

    private void Start()
    {
        UpdateColor();
    }

    private void Update()
    {
        if (test)
        {
            test = false;
            SetGravity(!gravity);
        }
    }

    public void SetGravity(bool gravityEnabled)
    {
        Debug.Log("Settings gravity: " + gravityEnabled.ToString());
        ResetVisited();
        bool hullBroken = SetGravityRecursive(gravityEnabled);
        Debug.Log("Hull: " + hullBroken.ToString());
        if (hullBroken && gravityEnabled)
        {
            // Found a broken outerhull -> Disable gravity
            Debug.Log("Hull is broken! Revert!");
            ResetVisited();
            SetGravityRecursive(false);
        }
    }

    public void ResetVisited()
    {
        foreach (Room room in FindObjectsOfType<Room>())
        {
            room.roomChecked = false;
        }
    }

    public bool SetGravityRecursive(bool newGravity)
    {
        roomChecked = true;
        gravity = newGravity;
        bool hullBroken = false;
        foreach (Wall wall in walls)
        {
            if (wall.CanGravityGetThrough())
            {
                if (wall.IsOuterHull())
                {
                    hullBroken = true;
                }

                if (hullBroken && newGravity)
                {
                    // Cant enable gravity if hull is broken
                    break;
                }
                foreach (Room room in wall.rooms)
                {
                    if (!room.roomChecked)
                    {
                        hullBroken = hullBroken || room.SetGravityRecursive(newGravity);
                        if (hullBroken && newGravity)
                        {
                            // Cant enable gravity if hull is broken
                            break;
                        }
                    }
                }
            }
        }

        UpdateColor();
        return hullBroken;
    }

    private void UpdateColor()
    {
        if (gravity) GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.2f);
        else GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.05f);
    }
   

    public bool HasGravity()
    {
        return gravity;
    }
}
