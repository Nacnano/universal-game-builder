using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class DragonGameManager : MonoBehaviour
{
    public static DragonGameManager Instance { get; private set; }

    [Header("Game State")]
    public bool isGameActive = false;
    public float distanceWalked = 0f;
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Difficulty Settings")]
    public float baseMoveSpeed = 5f;
    public float moveSpeedIncreaseRate = 0.1f;
    public float maxMoveSpeed = 15f;

    [Header("UI Elements")]
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public Image healthFill;

    public float CurrentMoveSpeed { get; private set; }

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
        CurrentMoveSpeed = baseMoveSpeed;
    }

    private void Start()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateUI();
    }

    private void Update()
    {
        if (isGameActive)
        {
            // Increase distance based on move speed
            distanceWalked += CurrentMoveSpeed * Time.deltaTime;
            
            // Increase global speed to increase difficulty
            CurrentMoveSpeed += moveSpeedIncreaseRate * Time.deltaTime;
            CurrentMoveSpeed = Mathf.Min(CurrentMoveSpeed, maxMoveSpeed);

            UpdateScoreUI();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        distanceWalked = 0f;
        currentHealth = maxHealth;
        CurrentMoveSpeed = baseMoveSpeed;
        
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        if (!isGameActive) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();

        // Optional Camera Shake
        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null) shake.Shake(0.2f, 0.2f);

        if (currentHealth <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        isGameActive = false;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null)
                finalScoreText.text = "Final Distance: " + Mathf.FloorToInt(distanceWalked) + "m";
        }
    }

    private void UpdateUI()
    {
        if (healthFill != null)
        {
            healthFill.DOKill();
            healthFill.DOFillAmount((float)currentHealth / maxHealth, 0.3f);
        }
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Distance: " + Mathf.FloorToInt(distanceWalked) + "m";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
