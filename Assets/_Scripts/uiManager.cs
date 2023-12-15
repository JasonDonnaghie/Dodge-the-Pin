using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static gameManager;

public class uiManager : MonoBehaviour
{
    
    public GameObject startScreen;
    public GameObject highScoreText;
    public GameObject scoreText;
    public GameObject endScreen;
    public GameObject pauseScreen;
    
    private int highScore;

    void Awake()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
        gameManager.OnPointsIncreased += OnPointsIncreased;
    }
    void Start()
    {
        startScreen.SetActive(true);
        scoreText.SetActive(true);
        endScreen.SetActive(false);
        pauseScreen.SetActive(false);
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "High Score: " + highScore.ToString();
    }

    private void OnGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.Start:
                scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
                startScreen.SetActive(true);
                scoreText.SetActive(true);
                endScreen.SetActive(false);
                pauseScreen.SetActive(false);
                break;
            case GameState.Playing:
                startScreen.SetActive(false);
                scoreText.SetActive(true);
                endScreen.SetActive(false);
                pauseScreen.SetActive(false);
                break;
            case GameState.Paused:
                startScreen.SetActive(false);
                scoreText.SetActive(true);
                endScreen.SetActive(false);
                pauseScreen.SetActive(true);
                break;
            case GameState.GameOver:

                if (gameManager.Instance.points > highScore)
                {
                    highScore = gameManager.Instance.points;
                    PlayerPrefs.SetInt("highScore", highScore);
                    highScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "New High Score!" + "\n" + "High Score: " + highScore.ToString();
                }else
                {
                    highScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "High Score: " + highScore.ToString();
                }

                startScreen.SetActive(false);
                scoreText.SetActive(true);
                endScreen.SetActive(true);
                pauseScreen.SetActive(false);
                break;
        }
    }

    void OnPointsIncreased()
    {
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = gameManager.Instance.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
