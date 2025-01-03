using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameStarted;
    public TextMeshProUGUI startText;
    public static bool gameOver;
    public GameObject gameOverPanel;

    public GameObject coinCounterPanel;
    public static int collectedCoins;
    public TextMeshProUGUI coinText;

    public GameObject scorePanel;
    public static string username;
    public static float score;
    public static float scoreValue;
    public TextMeshProUGUI scoreText;
    
    public TextMeshProUGUI ScoreNumber;
    public TextMeshProUGUI newHighScore;

    public GameObject PausedPanel;
    public GameObject PauseBtn;
    bool isPaused = false;


    public static List<Person> players = new List<Person>();
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        gameOver = false;
        Time.timeScale = 1;

        collectedCoins = 0;
        score = 0f;
        scoreValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = collectedCoins.ToString();
        scoreText.text = score.ToString("000000000000");
        if (gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause_Play();
            }
            coinCounterPanel.SetActive(true);
            scorePanel.SetActive(true);
            PauseBtn.SetActive(true);
            score += scoreValue * Time.deltaTime;
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            foreach (Person player in players)
            {
                if(player.name == username)
                {
                    if (Mathf.Round(score) > player.highestScore)
                    {
                        player.highestScore = Mathf.Round(score);
                        newHighScore.gameObject.SetActive(true);
                        //Debug.Log($"New High Score! {player.highestScore}");
                    }
                    ScoreNumber.text = Mathf.Round(score).ToString();
                }
            }
        }
        if (SwipeManager.tap || Input.anyKey)
        {
            gameStarted = true;
            Destroy(startText);
            Destroy(GameObject.Find("sprayCan"));
        }


    }

    public void Pause_Play()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            PausedPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PausedPanel.SetActive(false);
        }
    }
}
