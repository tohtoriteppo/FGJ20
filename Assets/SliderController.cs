using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public GameObject fuelSlider;
    public GameObject meteorSlider;
    public GameObject EMPSlider;
    public GameObject progressSlider;
    public EventMap events;

    private Text distanceText;
    private float startDistance;
    private bool init;
    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = FindObjectOfType<Canvas>().transform;
        fuelSlider = Instantiate(fuelSlider, canvas);
        meteorSlider = Instantiate(meteorSlider, canvas);
        EMPSlider = Instantiate(EMPSlider, canvas);
        progressSlider = Instantiate(progressSlider, canvas);
        events = FindObjectOfType<EventMap>();
        progressSlider = Instantiate(progressSlider, canvas);
    }

    // Update is called once per frame
    void Update()
    {
        if(!init)
        {
            startDistance = events.DistanceToTarget();
            init = true;
        }
        float emp = events.GetEmpValue();
        float fuel = events.GetFuelValue();
        float meteor = events.GetMeteorValue();
        EMPSlider.GetComponent<Slider>().value = emp;
        fuelSlider.GetComponent<Slider>().value = fuel;
        meteorSlider.GetComponent<Slider>().value = meteor;
        progressSlider.GetComponent<Slider>().value = (startDistance - events.DistanceToTarget())/ startDistance;
        if (events.DistanceToTarget() < 0.001f) FindObjectOfType<GameController>().EndGame(true);
    }
}
