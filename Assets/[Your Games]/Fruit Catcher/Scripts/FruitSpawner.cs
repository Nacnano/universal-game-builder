using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    
    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnWidthX = 3f;
    
    private float spawnTimer = 0f;

    private void Update()
    {
        if (FruitGameManager.Instance != null && !FruitGameManager.Instance.isGameActive)
            return;

        spawnTimer -= Time.deltaTime;
        
        if (spawnTimer <= 0f)
        {
            SpawnFruit();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnFruit()
    {
        if (fruitPrefab == null) return;

        float randomX = Random.Range(-spawnWidthX, spawnWidthX);
        Vector3 spawnPos = transform.position + new Vector3(randomX, 0, 0);

        Instantiate(fruitPrefab, spawnPos, Quaternion.identity);
    }
}
