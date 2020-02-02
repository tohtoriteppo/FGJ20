using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    private AudioSource musicPlayer;
    public AudioClip gameMusic;
    public AudioClip winMusic;
 
    // Start is called before the first frame update
    void Start()
    {
        //listener = GetComponent<AudioListener>();
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.clip = gameMusic;
        musicPlayer.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndGame(bool win)
    {
        if (win) Win();
        else Lose();
    }

    private void Lose()
    {
        SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
    }

    private void Win()
    {
        SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
    }
 
}
