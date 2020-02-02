using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public GameObject fuelSlider;
    public GameObject meteorSlider;
    public GameObject EMPSlider;

    // Start is called before the first frame update
    void Awake()
    {
        Transform canvas = FindObjectOfType<Canvas>().transform;
        fuelSlider = Instantiate(fuelSlider, canvas);
        meteorSlider = Instantiate(meteorSlider, canvas);
        EMPSlider = Instantiate(EMPSlider, canvas);
    }

    // Update is called once per frame
    void Update()
    {
        //update values
    }
}
