using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject[] pickUpPrefabs;
    [SerializeField] private float initialDelay = 2.0f;
    private float spawnInterval;

    [SerializeField] private Transform planeTransform;

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
        float planeScale = planeTransform.localScale.x;
        float range = 4f * planeScale;

        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);
        Vector3 spawnPos = new Vector3(randomX, spawnPosY, randomZ);

        int randomIndex = Random.Range(0, pickUpPrefabs.Length);
        GameObject prefabToSpawn = pickUpPrefabs[randomIndex];

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }
}
