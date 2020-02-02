using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool test = false;
    public bool roomGravity = true;
    public bool roomChecked = false;
    public List<Wall> walls = new List<Wall>();

    private Color originalColor;
    public Color noGravityColor;

    private GravityMachine globalGravitySource;
    private bool globalGravity
    {
        get
        {
            return globalGravitySource.HasGravity();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        originalColor = GetComponent<SpriteRenderer>().color;
        globalGravitySource = FindObjectOfType<GravityMachine>();
        UpdateColor();
    }

    private void Update()
    {
        SetGravity();
    }

    public void TraverseGravity(bool gravityEnabled)
    {
        ResetVisited();
        bool hullBroken = SetGravityRecursive(gravityEnabled);
        if (hullBroken && gravityEnabled)
        {
            // Found a broken outerhull -> Disable gravity
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

    public void SetGravity()
    {
        GetComponent<Collider2D>().enabled = globalGravity && roomGravity;
    }

    public bool SetGravityRecursive(bool newGravity)
    {
        roomChecked = true;
        roomGravity = newGravity;
        SetGravity();
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
        if (roomGravity) GetComponent<SpriteRenderer>().color = originalColor;
        else GetComponent<SpriteRenderer>().color = noGravityColor;
    }
   

    public bool HasGravity()
    {
        return roomGravity;
    }
}
