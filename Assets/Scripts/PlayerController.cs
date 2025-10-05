using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private Renderer render;
    [SerializeField] private float increaseSize = 0.1f;
    [SerializeField] private float speedDecreaseRate = 0.05f;
    [SerializeField] private Image EnergyBar;

    [Header("Movement")]
    [SerializeField] private float speed;
    private float horizontalInput;
    private float verticalInput;

    [Header("Energy & Visuals")]
    private float metallicValue = 0f;
    [SerializeField] private float metallicDecreaseLight = 0.2f;
    [SerializeField] private float metallicIncreaseLight = 0.2f;
    [SerializeField] private float impulseForce = 0.1f;

    private int counter;
    [SerializeField] private float minScale = 0.2f;
    private float initialScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
    }

    private void Start()
    {
        counter = 0;
        render.material.SetFloat("_Metallic", metallicValue);
        initialScale = transform.localScale.x;

        EnergyBar.fillAmount = 1f;
    }

    void Update()
    {
        metallicValue += metallicDecreaseLight * Time.deltaTime;
        render.material.SetFloat("_Metallic", metallicValue);

        EnergyBar.fillAmount = Mathf.Max(EnergyBar.fillAmount - metallicDecreaseLight * Time.deltaTime, 0f);

        float targetScale = Mathf.Lerp(initialScale, minScale, 1f - EnergyBar.fillAmount);
        transform.localScale = new Vector3(targetScale, targetScale, targetScale);

        if (metallicValue >= 1.0f)
        {
            GameManager._instance.GameOver();
        }

        HandlePlayerInput();
        HandleMovement();
    }

    void HandlePlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void HandleMovement()
    {
        rb.AddForce(speed * GetDirection().normalized * Time.deltaTime);
    }

    private Vector3 GetDirection()
    {
        return new Vector3(horizontalInput, 0f, verticalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            counter++;

            transform.localScale *= (1f + increaseSize);

            speed *= (1f - speedDecreaseRate);

            metallicValue = Mathf.Max(metallicValue - metallicIncreaseLight, 0f);
            EnergyBar.fillAmount = Mathf.Min(EnergyBar.fillAmount + metallicIncreaseLight, 1f);

            Destroy(other.gameObject);
        }
    }

    public int GetCounter()
    {
        return counter;
    }
}
