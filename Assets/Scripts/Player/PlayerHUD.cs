using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerHUDMode
{
    Arena,
    Default
}

public class PlayerHUD : MonoBehaviour
{
    public HealthBar playerHealthBar;
    public GameObject score;
    public GameObject waveInfo;
    public Text scoreCounter;
    public Text waveCounter;
    public Text enemyCounter;
    public Text ammoText;
    public Text gameOverHighscoreText;
    public Text gameOverScoreText;
    public GameObject gameOverScreen;
    public GameObject reticle;
    public PlayerHUDMode playerHUDMode;

    // Start is called before the first frame update
    void Start()
    {
        if(playerHUDMode == PlayerHUDMode.Arena)
        {
            score.SetActive(true);
            waveInfo.SetActive(true);
        }
        else
        {
            score.SetActive(false);
            waveInfo.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.SetCurrentHealthRatio(ArenaGameController.Instance.playerHealth.GetCurrentHealthRatio());
        waveCounter.text = "Wave " + ArenaGameController.Instance.wave;
        scoreCounter.text = ArenaGameController.Instance.score.ToString();
        enemyCounter.text = ArenaGameController.Instance.GetRemainingEnemies() + " Left";
    }

    public void ShowGameOverText()
    {
        gameOverScreen.SetActive(true);
        gameOverHighscoreText.text = ArenaGameController.Instance.highscore.ToString();
        gameOverScoreText.text = ArenaGameController.Instance.score.ToString();

        score.SetActive(false);
        playerHealthBar.gameObject.SetActive(false);
        waveCounter.gameObject.SetActive(false);
        enemyCounter.gameObject.SetActive(false);
        ammoText.gameObject.SetActive(false);
        reticle.SetActive(false);
    }

    public void ShowHUD()
    {
        gameOverScreen.SetActive(false);
        playerHealthBar.gameObject.SetActive(true);
        waveCounter.gameObject.SetActive(true);
        scoreCounter.gameObject.SetActive(true);
        ammoText.gameObject.SetActive(true);
    }
}
