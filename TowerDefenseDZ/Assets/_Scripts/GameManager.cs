using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Game
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyDiag;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    //Menus
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private Sprite menuPlay;
    [SerializeField] private Sprite menuGuide;
    [SerializeField] private Sprite menuQuit;

    float timer = 0;
    bool canSpawnDiag;
    float spawnTime = 2f;
    float totalTime = 0;
    int score = 0;
    int highScore = 0;
    int menuPosition = 0;
    public bool isGameActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isGameActive = false;
        highScore = SaveSystem.LoadHighScore();
        highScoreText.text = $"HIGH SCORE:\n{highScore}";
    }

    private void Update()
    {
        timer += Time.deltaTime;
        totalTime += Time.deltaTime;
        timeText.text = $"TIME: {Mathf.Floor(totalTime)} s";

        //Enemy Spawning
        if (timer > spawnTime )
        {
            SpawnEnemy();
            timer = 0;
            if (totalTime > 10 && !canSpawnDiag) 
            { 
                canSpawnDiag = true; 
            }
            if(totalTime > 20 && spawnTime != 3f)
            {
                spawnTime = 3f;
            }
            if (totalTime > 30 && spawnTime != 2.1f)
            {
                spawnTime = 2.1f;
            }
            if (totalTime > 40 && spawnTime != 1.5f)
            {
                spawnTime = 1.5f;
            }
            if (totalTime > 50 && spawnTime != 0.8f)
            {
                spawnTime = 0.8f;
            }
        }
        if (timer > spawnTime / 2 && canSpawnDiag && isGameActive)
        {
            SpawnEnemyDiag();
            canSpawnDiag = false;
        }

        //Menus
        if (Input.GetKeyDown(KeyCode.W))
        {
            menuPosition--;
            ChangeMenuPanel();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            menuPosition++;
            ChangeMenuPanel();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            MenuSelect();
        }
        if(instructionsPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenuPanel.SetActive(true);
            instructionsPanel.SetActive(false);
            menuPosition = 1;
        }
        if (Player.playerHealth <= 0)
        {
            GameOver();
        }
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            highScore = SaveSystem.LoadHighScore();
            highScoreText.text = $"HIGH SCORE:\n{highScore}";
            gameOverPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
            isGameActive = false;
            menuPosition = 0;
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemy);
    }

    void SpawnEnemyDiag()
    {
        Instantiate(enemyDiag);
        if (spawnTime == 2f)
        {
            spawnTime = 4f;
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = $"SCORE: {score}";
    }

    void StartGame()
    {
        Player.playerHealth = 4;
        totalTime = 0;
        timer = 0;
        spawnTime = 2f;
        canSpawnDiag = false;
        score = 0;
        isGameActive = true;
        mainMenuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
        isGameActive = false;
        if (score > highScore)
        {
            gameOverText.text = "NEW HIGHSCORE!!!\n\nPress ENTER to return to main menu";
            SaveSystem.SaveHighScore(score);
        }
        else
        {
            gameOverText.text = "\n\nPress ENTER to return to main menu";
        }
    }

    void ChangeMenuPanel()
    {
        if(menuPosition > 2)
        {
            menuPosition = 0;
        }
        else if (menuPosition < 0)
        {
            menuPosition = 2;
        }

        if (menuPosition == 0)
        {
            mainMenuPanel.GetComponent<Image>().sprite = menuPlay;
        }
        else if (menuPosition == 1)
        {
            mainMenuPanel.GetComponent<Image>().sprite = menuGuide;
        }
        else
        {
            mainMenuPanel.GetComponent<Image>().sprite = menuQuit;
        }
    }

    void MenuSelect()
    {
        if (menuPosition == 0)
        {
            StartGame();
        }
        else if (menuPosition == 1)
        {
            mainMenuPanel.SetActive(false);
            instructionsPanel.SetActive(true);
        }
        else
        {
            Application.Quit();
        }
    }

}
