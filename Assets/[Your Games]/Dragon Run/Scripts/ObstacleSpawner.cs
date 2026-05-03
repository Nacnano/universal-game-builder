using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] lowerWallPrefabs; // e.g. stalagmites, walls attached to the ground
    public GameObject[] upperWallPrefabs; // e.g. stalactites, walls attached to the ceiling
    public GameObject[] monsterPrefabs; // large monsters that block entirely

    [Header("Spawn Settings")]
    public float baseSpawnInterval = 2.5f;
    public float minSpawnInterval = 0.8f;
    public float spawnIntervalDecreaseRate = 0.05f;

    [Header("Spawn Position Options")]
    public float monsterSpawnY = 0f;
    public float lowerWallSpawnY = -2.5f;
    public float upperWallSpawnY = 2.5f;

    private float currentSpawnInterval;
    private float spawnTimer;

    private void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
    }

    private void Update()
    {
        if (DragonGameManager.Instance == null || !DragonGameManager.Instance.isGameActive)
            return;

        // Decrease spawn interval over time to increase difficulty (spawn faster)
        currentSpawnInterval -= spawnIntervalDecreaseRate * Time.deltaTime;
        currentSpawnInterval = Mathf.Max(currentSpawnInterval, minSpawnInterval);

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEntity();
            spawnTimer = currentSpawnInterval;
        }
    }

    private void SpawnEntity()
    {
        // 1. Decide to spawn either a Wall or a Monster (e.g. 40% Monster, 60% Wall)
        float chance = Random.value;
        GameObject prefabToSpawn = null;
        float yPos = 0f;

        if (chance < 0.4f && monsterPrefabs != null && monsterPrefabs.Length > 0)
        {
            // Spawn Monster
            int r = Random.Range(0, monsterPrefabs.Length);
            prefabToSpawn = monsterPrefabs[r];
            yPos = monsterSpawnY;
        }
        else 
        {
            // Spawn Wall: 50% chance for upper wall, 50% chance for lower wall
            bool spawnUpper = Random.value > 0.5f;

            if (spawnUpper && upperWallPrefabs != null && upperWallPrefabs.Length > 0)
            {
                int r = Random.Range(0, upperWallPrefabs.Length);
                prefabToSpawn = upperWallPrefabs[r];
                yPos = upperWallSpawnY; // Fixed high position attached to ceiling
            }
            else if (!spawnUpper && lowerWallPrefabs != null && lowerWallPrefabs.Length > 0)
            {
                int r = Random.Range(0, lowerWallPrefabs.Length);
                prefabToSpawn = lowerWallPrefabs[r];
                yPos = lowerWallSpawnY; // Fixed low position attached to ground
            }
            // Fallbacks in case one array is empty but the other isn't
            else if (lowerWallPrefabs != null && lowerWallPrefabs.Length > 0)
            {
                int r = Random.Range(0, lowerWallPrefabs.Length);
                prefabToSpawn = lowerWallPrefabs[r];
                yPos = lowerWallSpawnY;
            }
            else if (upperWallPrefabs != null && upperWallPrefabs.Length > 0)
            {
                int r = Random.Range(0, upperWallPrefabs.Length);
                prefabToSpawn = upperWallPrefabs[r];
                yPos = upperWallSpawnY;
            }
        }

        if (prefabToSpawn != null)
        {
            Vector3 spawnObjPos = new Vector3(transform.position.x, yPos, transform.position.z);
            GameObject spawnedObj = Instantiate(prefabToSpawn, spawnObjPos, Quaternion.identity);
            
            // Ensure it has ScrollingObject component to move based on speed
            if (spawnedObj.GetComponent<ScrollingObject>() == null)
            {
                spawnedObj.AddComponent<ScrollingObject>();
            }
        }
    }
}
