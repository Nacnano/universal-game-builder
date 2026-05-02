using UnityEngine;
using System.Collections;

public class FruitSpawner : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (FruitGameManager.Instance == null || !FruitGameManager.Instance.isGameActive)
            {
                yield return null;
                continue;
            }

            GameObject[] prefabs = FruitGameManager.Instance.fruitPrefabs;

            if (prefabs != null && prefabs.Length > 0)
            {
                float minX = FruitGameManager.Instance.spawnMinX;
                float maxX = FruitGameManager.Instance.spawnMaxX;

                float randomX = Random.Range(minX, maxX);
                Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0f);
                int randomIndex = Random.Range(0, prefabs.Length);
                Instantiate(prefabs[randomIndex], spawnPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(FruitGameManager.Instance.spawnInterval);
        }
    }
}
