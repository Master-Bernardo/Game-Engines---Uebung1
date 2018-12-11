using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float highscore;

    private HashSet<Unit> units; //this hashSet saves all fighters currently inGame
    private HashSet<Interactable> interactables; //interactables inGame

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
   
        //DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        units = new HashSet<Unit>();
        interactables = new HashSet<Interactable> ();
        highscore = PlayerPrefs.GetFloat("highscore", 0);
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
        SetHighscore();
        playerHUD.ShowGameOverText();
        if(spawnManager!=null)spawnManager.Deactivate();
        currentState = State.GameOver;
    }
    public void ResetGame()
    {
        /*
        wave = 0;
        score = 0;
        playerHealth.ResetHealth();
        playerWeaponSystem.Reset();
        playerHUD.ShowHUD();
        spawnManager.Activate(); 
        currentState = State.Ingame;
        */
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetHighscore()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetFloat("highscore", highscore);
        }
    }

    public void AddScore(float scoreValue)
    {
        score += scoreValue;
    }

    public void IncrementWave()
    {
        wave++;
    }

    public HashSet<Unit> GetAllUnits()
    {
        return units;
    }

    public void AddUnit(Unit fighter)
    {
        units.Add(fighter);
    }

    public void RemoveUnit(Unit fighter)
    {
        units.Remove(fighter);
    }

    public HashSet<Interactable> GetAllInteractables()
    {
        return interactables;
    }

    public void AddInteractable(Interactable interactable)
    {
        interactables.Add(interactable);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        interactables.Remove(interactable);
    }

    public int GetRemainingEnemies()
    {
        int count = 0;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.GetComponent<Unit>().alive)
            {
                count++;
            }
        }

        return count;
    }
}
