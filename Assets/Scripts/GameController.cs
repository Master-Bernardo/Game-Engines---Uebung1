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
    public Health playerHealth;
    public WeaponSystem playerWeaponSystem;
    public PlayerHUD playerHUD;
    public SpawnManager spawnManager;
    public float wave;
    public float score;

    private HashSet<Enemy> fighters; //this hashSet saves all fighters currently inGame

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        fighters = new HashSet<Enemy>();
    }


    void Update()
    {
        switch(currentState)
        {
            case State.Ingame:
                if(playerHealth.GetCurrentHealth() <= 0)
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
        playerHealth.ResetHealth();
        playerWeaponSystem.Reset();
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

    public HashSet<Enemy> GetAllFighters()
    {
        return fighters;
    }

    public void AddFighter(Enemy fighter)
    {
        fighters.Add(fighter);
    }

    public void RemoveFighter(Enemy fighter)
    {
        fighters.Remove(fighter);
    }
}
