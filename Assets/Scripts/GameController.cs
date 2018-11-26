using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum State 
    {
        Ingame, GameOver
    }
    public static GameController Instance;
    public State currentState = State.Ingame;
    public GameObject player;
    public PlayerBehavior playerScript;
    public PlayerHUD playerHUD;
    public SpawnManager spawnManager;
    public float wave;
    public float score;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); 
        }
   
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Ingame:
                if(playerScript.currentHealth <= 0)
                {
                    GameOver();
                }
                break;
            
            case State.GameOver:
                if (Input.GetKey(KeyCode.N))
                {
                    ResetGame();
                }
                break;
        }
    }

    public void GameOver()
    {
        playerHUD.ShowGameOverText();
        spawnManager.Deactivate();
        currentState = State.GameOver;
    }
    public void ResetGame()
    {
        wave = 0;
        score = 0;
        playerScript.ResetHealth();
        playerHUD.ShowHUD();
        spawnManager.Activate(); 
        currentState = State.Ingame;
    }

    public void AddScore(float scoreValue)
    {
        score += scoreValue;
    }

    public void IncrementWave()
    {
        wave++;
    }
}
