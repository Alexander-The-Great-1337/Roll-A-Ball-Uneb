using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Transform winContainer;
    [SerializeField] private Transform gameOverContainer;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Transform pickUpManager;
    [SerializeField] private Button winRestartButton;
    [SerializeField] private Button winQuitButton;
    [SerializeField] private Button gameOverRestartButton;
    [SerializeField] private Button gameOverQuitButton;

    [SerializeField] private float roundTime = 60.0f;
    [SerializeField] private int maxPickUps = 34;

    private PlayerController playerController;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
        winContainer.gameObject.SetActive(false);
        gameOverContainer.gameObject.SetActive(false);
    }

    void Start()
    {
        countText.text = GetFormatedScore();

        gameOverRestartButton.onClick.AddListener(RestartLevel);
        gameOverQuitButton.onClick.AddListener(GoToMenu);
        winRestartButton.onClick.AddListener(RestartLevel);
        winQuitButton.onClick.AddListener(GoToMenu);
    }

    public void RegisterPlayer(PlayerController player)
    {
        playerController = player;
    }

    private void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    private void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        CheckWinCondition();
            
        if (roundTime > 0.0f)
        {
            roundTime -= Time.deltaTime;
            if (roundTime < 0.0f)
            {
                roundTime = 0.0f;
                TimeIsUp();
            }
        }
        countText.text = GetFormatedScore();
        timerText.text = $"Time: {roundTime:F2}";
    }

    private void TimeIsUp()
    {
        GameOver();
    }

    private string GetFormatedScore()
    {
        return $"{playerController.GetCounter()}/{maxPickUps}";
    }

    public void GameOver()
    {
        gameOverContainer.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Win()
    {
        winContainer.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void CheckWinCondition()
    {
        if (playerController.GetCounter() >= maxPickUps)
        {
            Win();
        }
    }
}
