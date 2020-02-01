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
        gameObject.GetComponent<MeteroSpawner>().SpawnMetero(Random.Range(1.0f, 2.0f));
    }

    void EmpDamage(float f)
    {
        //Tick emp damage
        Debug.Log("So much damage " + f);
    }

    void Fuel(float f)
    {
        //Increase available fuel
        Debug.Log("Very much fuel " + f);
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
                EmpDamage(pixel.g);
                Fuel(pixel.b);
            }

        }

    }
}
