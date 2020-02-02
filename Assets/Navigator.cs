using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : Machine
{

    private int direction = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<EventMap>().turnDirection = direction;
        direction = 0;
    }

    public void Navigate(int dir)
    {
        direction = dir;
    }


    protected override void UpdateState()
    {
    }

}


