using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{

    private bool wait = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Ready", 5f);
    }

    private void Ready()
    {
        wait = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("p1_button_x") && wait)
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
