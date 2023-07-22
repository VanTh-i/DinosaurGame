using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    private float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    private Player player;
    private Spawner spawner;

    public GameObject gameOverUI;
    public TextMeshProUGUI scoreText, highScoreText;

    private float score, highscore;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.RoundToInt(score).ToString("D5");
    }
    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        enabled = true;
        score = 0f;        

        gameOverUI.SetActive(false);
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        UpdateHighScore();
    }
    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        gameOverUI.SetActive(true);
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        UpdateHighScore();
    }
    void UpdateHighScore()
    {
        highscore = PlayerPrefs.GetFloat("HI", 0);

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetFloat("HI", highscore);
        }
        highScoreText.text = Mathf.RoundToInt(highscore).ToString("D5");
    }
}
