﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    private int gravity_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Gravity count" + gravity_count);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInChildren<Room>())
        {
            if (gravity_count++ == 0)
            {
                GetComponentInChildren<Movement>().SetGravity(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInChildren<Room>())
        {
            --gravity_count;
            if (gravity_count < 0)
            {
                gravity_count = 0;
            }
            if (gravity_count == 0)
            {
                GetComponentInChildren<Movement>().SetGravity(false);
            }
        }
    }
}
