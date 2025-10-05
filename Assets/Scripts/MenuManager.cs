using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        startButton.onClick.AddListener(StartLevel);
        exitButton.onClick.AddListener(Exit);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene("Main");
    }
    private void Exit()
    {
        Application.Quit();
    }
}
