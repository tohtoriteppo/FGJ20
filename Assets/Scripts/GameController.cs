using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    
    private bool gameEnded = false;
    private bool gameStarted = false;
    private bool initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Initialize()
    {
        Transform canvas = FindObjectOfType<Canvas>().transform;
        startScreen = Instantiate(startScreen, canvas);
        winScreen = Instantiate(winScreen, canvas);
        loseScreen = Instantiate(loseScreen, canvas);
        startScreen.SetActive(true);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        initialized = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (!initialized) Initialize();
        if(!gameStarted)
        {
            if (Input.GetButtonDown("p1_button_x"))
            {
                startScreen.SetActive(false);
                gameStarted = true;
            }
            
        }
        if(gameEnded)
        {
            if (Input.GetButtonDown("p1_button_x"))
            {
                startScreen.SetActive(true);
                winScreen.SetActive(false);
                loseScreen.SetActive(false);
                gameEnded = false;
            }
            
        }
    }
    public void EndGame(bool win)
    {
        if (win) winScreen.SetActive(true);
        else loseScreen.SetActive(true);
        gameStarted = false;
        gameEnded = true;
    }
    public bool GameStarted()
    {
        return gameStarted;
    }
}
