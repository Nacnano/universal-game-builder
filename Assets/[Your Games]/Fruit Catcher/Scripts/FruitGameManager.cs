using UnityEngine;
using TMPro;

public class FruitGameManager : MonoBehaviour
{
    public static FruitGameManager Instance { get; private set; }

    [Header("Game State")]
    public bool isGameActive = false;
    public int score = 0;
    public int health = 3;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        health = 3;
        UpdateUI();
        
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        if (!isGameActive) return;
        score += amount;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        if (!isGameActive) return;
        health -= amount;
        UpdateUI();

        if (health <= 0)
        {
            GameOver();
        }
    }

    public void MissFruit()
    {
        // Optional: penalty for missing a fruit
        TakeDamage(1);
    }

    private void GameOver()
    {
        isGameActive = false;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (healthText != null) healthText.text = "Health: " + health;
    }
}
