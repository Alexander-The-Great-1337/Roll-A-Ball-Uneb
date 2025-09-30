using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerController;

    private Vector3 displacement;

    void Start()
    {
        displacement = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerController.position + displacement;
    }
}
