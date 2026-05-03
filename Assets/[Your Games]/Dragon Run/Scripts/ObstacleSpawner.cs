using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] wallPrefabs; // e.g. tall walls with holes at lower or upper part
    public GameObject[] monsterPrefabs; // large monsters that block entirely

    [Header("Spawn Settings")]
    public float baseSpawnInterval = 2.5f;
    public float minSpawnInterval = 0.8f;
    public float spawnIntervalDecreaseRate = 0.05f;

    [Header("Spawn Position Options")]
    public float monsterSpawnY = 0f;
    public float wallSpawnY = 0f;

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
        else if (wallPrefabs != null && wallPrefabs.Length > 0)
        {
            // Spawn Wall
            int r = Random.Range(0, wallPrefabs.Length);
            prefabToSpawn = wallPrefabs[r];
            yPos = wallSpawnY;
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
