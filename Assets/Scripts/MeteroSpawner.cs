using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteroSpawner : MonoBehaviour
{
    public GameObject meteroPrefab;
    public float radii = 11f;
    public Vector2 upLeft;
    public Vector2 botRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnMetero(float speed)
    {
        float rando = Random.Range(0f, Mathf.PI * 2);
        float spawnX = Mathf.Cos(rando) * radii;
        float spawnY = Mathf.Sin(rando) * radii;
        float targetX = Random.Range(upLeft.x, botRight.x);
        float targetY = Random.Range(upLeft.y, botRight.y);
        GameObject newObject = Instantiate(meteroPrefab);
        newObject.transform.position = new Vector3(spawnX, spawnY);
        Rigidbody2D body = newObject.GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(targetX - spawnX, targetY - spawnY).normalized * 2;
        newObject.GetComponent<MeteroScript>().speed = body.velocity.magnitude;
        newObject.GetComponent<MeteroScript>().directionVector = body.velocity.normalized;
        newObject.GetComponent<MeteroScript>().currentHealth = Random.Range(50f, 200f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
