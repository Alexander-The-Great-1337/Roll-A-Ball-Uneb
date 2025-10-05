using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Transform pickUpManager;
    [SerializeField] private float roundTime = 60.0f;
    [SerializeField] private int maxPickUps = 34;

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
        DontDestroyOnLoad(gameObject);
        winText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    void Start()
    {
        countText.text = GetFormatedScore();
    }

    // Update is called once per frame
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
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Win()
    {
        winText.gameObject.SetActive(true);
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
