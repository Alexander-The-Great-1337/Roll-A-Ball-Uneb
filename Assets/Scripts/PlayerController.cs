using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float speed;
    private float horizontalInput;
    private float vertticalInput;

    private int counter;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 move = new Vector3(horizontalInput, 0f, vertticalInput);
        rb.AddForce(speed * move * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            counter++;
            Destroy(other.gameObject);
        }
    }

    public int GetCounter()
    {
        return counter;
    }
}
