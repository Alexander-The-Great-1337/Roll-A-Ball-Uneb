using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] pickUpPrefabs; // <- array to hold multiple prefabs
    [SerializeField] private float initialDelay = 2.0f;
    private float spawnInterval;

    // Reference to your plane (the area where items will spawn)
    [SerializeField] private Transform planeTransform;

    // Adjust these if needed
    private float spawnPosY = 0.5f;

   void Awake()
    {
        InstantiateRandomPickUp();
    }

    void Start()
    {
        spawnInterval = initialDelay;
    }

    void Update()
    {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0)
        {
            SpawnPickUpItem();
            spawnInterval = initialDelay;
        }
    }

    void SpawnPickUpItem()
    {
        if (pickUpPrefabs == null || pickUpPrefabs.Length == 0)
        {
            Debug.LogWarning("No pick-up prefabs assigned in the SpawnManager.");
            return;
        }

        if (planeTransform == null)
        {
            Debug.LogWarning("No plane assigned in the SpawnManager.");
            return;
        }

        InstantiateRandomPickUp();
    }

    void InstantiateRandomPickUp()
    {
        // Get plane boundaries based on its scale
        float planeScale = planeTransform.localScale.x; // Assuming it's uniform (x = z)
        float range = 4f * planeScale; // Plane default size is 10x10 units, so scale * 5 = half-size

        // Random position inside plane bounds
        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);
        Vector3 spawnPos = new Vector3(randomX, spawnPosY, randomZ);

        // Choose a random prefab to spawn
        int randomIndex = Random.Range(0, pickUpPrefabs.Length);
        GameObject prefabToSpawn = pickUpPrefabs[randomIndex];

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
