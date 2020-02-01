using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPStorm : MonoBehaviour
{
    public float damagePerSecond = 10;
    private Machine[] machines;
    
    // Start is called before the first frame update
    void Start()
    {
        machines = FindObjectsOfType<Machine>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Machine machine in machines)
        {
            machine.Damage(Time.deltaTime * damagePerSecond);
        }
    }
}
