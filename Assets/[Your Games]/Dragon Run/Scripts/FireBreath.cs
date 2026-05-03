using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireBreath : MonoBehaviour
{
    private Rigidbody2D rb;
    public float fireSpeed = 15f;
    public float lifetime = 3f;
    
    [Header("Visuals (Optional)")]
    public GameObject impactEffectPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Using Dynamic allows velocity to work perfectly without gravity pulling it down
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
    }

    private void Start()
    {
        // Shoot forward (right) using the speed relative to global GameManager speed to ensure it overtakes objects
        Vector2 speedDir = transform.right * (fireSpeed + (DragonGameManager.Instance != null ? DragonGameManager.Instance.CurrentMoveSpeed : 0));
        rb.velocity = speedDir;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Destroy enemy upon collision
            Destroy(collision.gameObject);
            
            // Score bonus or visual effect
            if (DragonGameManager.Instance != null)
                DragonGameManager.Instance.distanceWalked += 10f; // Bonus distance for killing monster
            
            // Impact VFX
            if (impactEffectPrefab != null)
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Obstacle")) // Walls
        {
            // Fire breath shouldn't destroy walls, just impact them
            if (impactEffectPrefab != null)
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
