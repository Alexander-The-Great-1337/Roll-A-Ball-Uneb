using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerController playerController;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] Transform pickUpManager;

    private int pickUpAmountRemain;

    private void Awake()
    {
        winText.gameObject.SetActive(false);
    }

    void Start()
    {
        countText.text = GetFormatedScore();
        GetPickUpItemsAmount();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        countText.text = GetFormatedScore();
    }

    private void GetPickUpItemsAmount()
    {
        foreach(Transform child in pickUpManager)
        {
            if (child.gameObject.activeSelf)
                pickUpAmountRemain++;
        }
    }

    private void CheckWinCondition()
    {
        bool pickUpAllItems = playerController.GetCounter() >= pickUpAmountRemain;

        if (pickUpAllItems)
        {
            winText.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private string GetFormatedScore()
    {
        return $"Score: {playerController.GetCounter()}";
    }
}
