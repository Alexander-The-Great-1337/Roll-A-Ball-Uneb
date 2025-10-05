using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private Renderer render;
    [SerializeField] private float increaseSize = .1f;
    [SerializeField] private float speedDecreaseRate = 0.05f;
    [SerializeField] private Scrollbar EnergyBar;

    [Header("Movement")]
    [SerializeField] private float speed;
    private float horizontalInput;
    private float vertticalInput;

    private float metallicValue = 0f;
    [SerializeField] private float metallicDecreaseLight = 0.2f;
    [SerializeField] private float metallicIncreaseLight = 0.2f;
    [SerializeField] private float impulseForce = 0.1f;

    private int counter;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
    }

    private void Start()
    {
        counter = 0;
        render.material.SetFloat("_Metallic", metallicValue);
    }

    void Update()
    {
        metallicValue += metallicDecreaseLight * Time.deltaTime;

        render.material.SetFloat("_Metallic", metallicValue);

        Debug.Log("Metallic Value: " + metallicValue);

        EnergyBar.size -= metallicDecreaseLight * Time.deltaTime;

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
        vertticalInput = Input.GetAxis("Vertical");
    }

    void HandleMovement()
    {
        Move();
    }

    void Move()
    {
        rb.AddForce(speed * GetDirection().normalized * Time.deltaTime);
    }

    private Vector3 GetDirection()
    {
        return new Vector3(horizontalInput, 0f, vertticalInput);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            counter++;
            transform.localScale *= (1f + increaseSize);

            speed *= (1f - speedDecreaseRate);

            metallicValue -= metallicIncreaseLight;
            EnergyBar.size += metallicIncreaseLight;

            Destroy(other.gameObject);
        }
    }

    public int GetCounter()
    {
        return counter;
    }
}
