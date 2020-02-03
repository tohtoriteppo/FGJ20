using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagement : MonoBehaviour
{
    private List<GameObject> objectsClose;
    private GameObject item;
    private Movement movement;
    private int playerNum;
    private PlayerManager manager;
    private float throwForce;
    private float throwChargeTime;
    private float repairAmount;
    private bool charging;
    private float timeStarted;
    private string repairString;
    private string pickupString;

    // Start is called before the first frame update
    void Start()
    {
        movement = transform.parent.GetComponent<Movement>();
        objectsClose = new List<GameObject>();
        manager = FindObjectOfType<PlayerManager>();
        throwForce = manager.GetThrowForce();
        throwChargeTime = manager.GetThrowChargeTime();
        repairAmount = manager.GetRepairAmount();
        repairString = "_button_"+ manager.GetRepairButton();
        pickupString = "_button_"+ manager.GetPickupButton();
        item = null; // !!!
    }

    // Update is called once per frame
    void Update()
    {
        playerNum = movement.GetPlayerNum();
        Throw();
        PickUp();
        Repair();
        Navigate();
    }
    private void FixedUpdate()
    {
        if (item != null)
        {
            item.transform.position = transform.position;
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void Repair()
    {
        if (Input.GetButton("p" + playerNum + repairString) /* && item != null */)
        { //                                                  ^ Uncomment when tools are tooling
            foreach (GameObject obj in objectsClose)
            {
                Damageable dmg = obj.GetComponent<Damageable>();
                if (dmg != null && dmg.CanBeRepaired())
                {
                    dmg.Repair(repairAmount);
                }
               
            }
        }
    }

    private void Throw()
    {
        if (Input.GetButtonDown("p" + playerNum + pickupString) && item != null)
        {
            charging = true;
            timeStarted = Time.time;
        }
        else if (Input.GetButtonUp("p" + playerNum + pickupString) && item != null && charging)
        {
            float horizontal = Input.GetAxis("p" + playerNum + "_joystick_horizontal");
            float vertical = Input.GetAxis("p" + playerNum + "_joystick_vertical");
            if (horizontal == 0 && vertical == 0) horizontal = transform.parent.GetComponent<SpriteRenderer>().flipX ? -1 : 1;
            float timeElapsed = Mathf.Min((Time.time-timeStarted), throwChargeTime);
            Debug.Log("throwForce: " + throwForce);
            Debug.Log("timeElapsed: " + timeElapsed);
            Debug.Log("horizontal: " + horizontal);
            Debug.Log("vertical: " + vertical);
            float force = throwForce * timeElapsed;
            item.GetComponent<Collider2D>().enabled = true;
            item.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * force, vertical * force));
            item.GetComponent<Rigidbody2D>().gravityScale = 1;
            item = null;
            charging = false;
        }
    }

    private void Navigate()
    {
        float vertical = Input.GetAxis("p" + playerNum + "_joystick_vertical") + Input.GetAxis("p" + playerNum + "_vertical");
        if(vertical != 0)
        {
            foreach (GameObject obj in objectsClose)
            {
                if (obj.tag == "navigator")
                {
                    obj.GetComponent<Navigator>().Navigate((int)vertical);
                }
            }
        }
        
    }

    private void PickUp()
    {
        if (Input.GetButtonDown("p" + movement.GetPlayerNum() + pickupString))
        {
            foreach(GameObject obj in objectsClose)
            {
                if(obj.tag == "item")
                {
                    if(item != null) item.GetComponent<Collider2D>().enabled = true;
                    item = obj;
                    item.GetComponent<Collider2D>().enabled = false;
                    item.GetComponent<Rigidbody2D>().gravityScale = 0;
                    break;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!objectsClose.Contains(collision.gameObject)) objectsClose.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectsClose.Contains(collision.gameObject)) objectsClose.Remove(collision.gameObject);
    }

}
