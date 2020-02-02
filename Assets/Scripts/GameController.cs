using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    private AudioSource musicPlayer;
    public AudioClip gameMusic;
    public AudioClip winMusic;
    

    //private AudioListener listener;
    
    private bool gameEnded = false;
    private bool gameStarted = false;
    private bool initialized = false;



    // Start is called before the first frame update
    void Start()
    {
        //listener = GetComponent<AudioListener>();
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.clip = gameMusic;
        musicPlayer.Play();
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
        if (win) Win();
        else Lose();
        gameStarted = false;
        gameEnded = true;
    }

    private void Lose()
    {
        loseScreen.SetActive(true);
    }

    private void Win()
    {
        winScreen.SetActive(true);
        musicPlayer.Stop();
        musicPlayer.clip = winMusic;
        musicPlayer.Play();
    }
    public bool GameStarted()
    {
        return gameStarted;
    }
}
