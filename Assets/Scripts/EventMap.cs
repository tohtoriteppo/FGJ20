using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMap : MonoBehaviour
{

    public Texture2D mapTexture; //R meteors, G fuel, B emp damage
    public Vector2 shipLocation = new Vector2(0.0f, 0.0f);
    [Range(0.5f, 10f)]
    public float speed = 1.0f;
    [Range(-45,45)]
    public int angleDegrees = 0;
    float angle = 0.0f;
    [Range(0.2f,5f)]
    public float derp = 1.0f;
    float decreasingTimer = 0.0f;
    public Vector2 speedVector = Vector2.zero;
    Vector2 helper = Vector2.zero;

    public int radius = 5;

    float fuel = 0.0f;
    float emp = 0.0f;
    float meteors = 0.0f;
    //float decreasingTimer = 0.0f;

    Color[] pixels;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Withd " + mapTexture.width + " Height " + mapTexture.height);
        shipLocation = new Vector2(0, mapTexture.height/2);
        speedVector = new Vector2(speed, 0.0f);
        pixels = mapTexture.GetPixels(0, 0, mapTexture.width, mapTexture.height);
        /*
        for (int y = 0; y < mapTexture.height; y++)
        {
            for (int x = 0; x < mapTexture.width; x++)
            {
                Color pixel = pixels[y * mapTexture.width + x];
            }
        }
        */
    }


    void Meteor()
    {
        //Spawn meteor
        gameObject.GetComponent<MeteroSpawner>().SpawnMetero(speed/10.0f);
    }
    
    public float GetEmpValue()
    {
        return emp;
    }

    public float GetFuelValue()
    {
        return fuel;
    }

    public float GetMeteorValue()
    {
        return meteors;
    }

    void ResourceUpdater()
    {
        //Increase available fuel
        //Debug.Log("Very much fuel " + f);

        int i = 0;
        Vector3 sum = Vector3.zero;
        
        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                if (new Vector2(y,x).magnitude <= radius)
                {
                    if (shipLocation.x + x >= 0 && shipLocation.x + x < mapTexture.width && shipLocation.y + y >= 0 && shipLocation.y + y < mapTexture.height)
                    {
                        Color temp = pixels[((int)shipLocation.y + y) * mapTexture.width + (int)shipLocation.x + x];
                        sum.x += temp.r;
                        sum.y += temp.g;
                        sum.z += temp.b;
                        i++;
                    }
                }
            }
        }
        sum /= i;
        meteors = sum.x;
        fuel = sum.y;
        emp = sum.z;
    }

    

    public float DistanceToTarget()
    {
        float currentMax = 1.0f / Mathf.Cos(angle) * mapTexture.width;
        float currentDist = ((float) mapTexture.width - shipLocation.x) / Mathf.Cos(angle);
        return currentDist / currentMax;
    }
   

    void Item(float f)
    {
        int i = (int)(f * 255);
        Debug.Log("There be item here "+i);
    }

    void Goal()
    {
        Debug.Log("Goal yeah");
        speed = 0.0f;
        speedVector = Vector2.zero;
    }

    public void updateVector()
    {
        speedVector.x = Mathf.Cos(angle) * speed;
        speedVector.y = Mathf.Sin(angle) * speed;
    }

    //Radians
    public void updateAngle(float f)
    {
        angle = f;
    }

    //Degrees
    public void updateAngle(int i)
    {
        updateAngle(i * Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void Update()
    {
        updateAngle(angleDegrees);
        updateVector();
        helper = Time.deltaTime * speedVector;
        int newX = (int)(helper.x + shipLocation.x);
        if (newX >= mapTexture.width - 1)
        {
            Goal();
        }
        else
        {
            float floatNewY = (helper.y + shipLocation.y) % mapTexture.height;
            if (floatNewY < 0.0f)
                floatNewY += mapTexture.height;
            int newY = (int)floatNewY;
            Color pixel = pixels[newY * mapTexture.width + newX];
            if ((newX != (int)shipLocation.x) || (newY != (int)shipLocation.y))
            {
                //trigger events yeah
                if (Random.Range(0.0f, 1.0f) <= pixel.r)
                {
                    Debug.Log("Meteor at " + pixel.r);
                    Meteor();
                }
                if (pixel.a != 1.0f)
                {
                    Item(pixel.a);
                }
            }
            shipLocation.x += helper.x;
            shipLocation.y = floatNewY;
            decreasingTimer -= Time.deltaTime;
            if (decreasingTimer <= 0.0f)
            {
                decreasingTimer += derp;
                Debug.Log(DistanceToTarget());
            }
            //Transform camera = gameObject.transform.GetChild(0);
            //camera.localPosition = new Vector3(-2.85f + 1.0f*newX/100, 1 - 1.0f*newY/100, -1f);
            //camera.rotation = Quaternion.Euler(0, 0, -angleDegrees);
            ResourceUpdater();
        }

    }
}
