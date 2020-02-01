using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedWallBreak : MonoBehaviour
{

    private Wall wall;
    public float timer = 5f;
    public bool causeDamage = true;
    
    // Start is called before the first frame update
    void Start()
    {
        wall = GetComponent<Wall>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (causeDamage && wall)
            {
                wall.Damage(500000000);
            }

            if (!causeDamage && wall)
            {
                wall.Repair(500000000);
            } 
            Destroy(this);
        }
    }
}
