using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public HealthBar playerHealthBar;
    public Text waveCounter;
    public Text scoreCounter;
    public Text gameOverText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.SetCurrentHealthRatio(GameController.Instance.playerScript.GetCurrentHealthRatio());
        waveCounter.text = "Wave " + GameController.Instance.wave;
        scoreCounter.text = GameController.Instance.score.ToString();
    }

    public void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true);
        playerHealthBar.gameObject.SetActive(false);
        waveCounter.gameObject.SetActive(false);
        scoreCounter.gameObject.SetActive(false);
    }

    public void ShowHUD()
    {
        gameOverText.gameObject.SetActive(false);
        playerHealthBar.gameObject.SetActive(true);
        waveCounter.gameObject.SetActive(true);
        scoreCounter.gameObject.SetActive(true);
    }
}
