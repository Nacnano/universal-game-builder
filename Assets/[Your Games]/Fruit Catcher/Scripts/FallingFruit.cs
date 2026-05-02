using UnityEngine;

public class FallingFruit : MonoBehaviour
{
    public float fallSpeed = 5f;
    public int scoreValue = 10;
    public float rotationSpeed = 180f;
    
    public GameObject catchEffectPrefab;
    public GameObject missEffectPrefab;

    private bool isCaught = false;

    private void Update()
    {
        if (isCaught) return;

        // Fall downwards regardless of rotation
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);

        // Rotate for visual juice
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Destroy if it goes too far down (missed)
        if (transform.position.y < -10f)
        {
            Miss();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hand needs to have the "Player" tag
        if (collision.CompareTag("Player") && !isCaught)
        {
            isCaught = true;

            if (FruitGameManager.Instance != null)
            {
                FruitGameManager.Instance.AddScore(scoreValue);
            }

            if (catchEffectPrefab != null)
            {
                Instantiate(catchEffectPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    private void Miss()
    {
        isCaught = true;

        if (FruitGameManager.Instance != null)
        {
            FruitGameManager.Instance.MissFruit();
        }

        if (missEffectPrefab != null)
        {
            Instantiate(missEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
