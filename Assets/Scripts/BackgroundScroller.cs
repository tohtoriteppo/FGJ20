using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float startX;
    public float endX;
    public float scrollTime;


    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = (Time.deltaTime / scrollTime) * (endX - startX);
        transform.position += new Vector3(deltaX, 0, 0);

        if (transform.position.x < endX)
        {
            Reset();
        }
    }

    private void Reset()
    {
        transform.position = new Vector2(startX, transform.position.y);
    }
}
