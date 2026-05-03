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
            // Score bonus or visual effect
            if (DragonGameManager.Instance != null)
                DragonGameManager.Instance.distanceWalked += 10f; // Bonus distance for killing monster
            
            // Impact VFX
            if (impactEffectPrefab != null)
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);

            // Handle the death animation instead of instant vanishing
            collision.enabled = false; // Disable collision so the dragon doesn't get hurt passing through the dying corpse
            
            Spine.Unity.SkeletonAnimation spineMonster = collision.GetComponentInChildren<Spine.Unity.SkeletonAnimation>();
            if (spineMonster != null)
            {
                spineMonster.AnimationState.SetAnimation(0, "Dead", false);
                
                // Optional: Stop the monster from moving forward toward the dragon, or let it scroll dead
                ScrollingObject scroller = collision.GetComponent<ScrollingObject>();
                if (scroller != null) Destroy(scroller); // Stops moving while dying
                
                // Destroy after 2 seconds to let the death animation finish
                Destroy(collision.gameObject, 1.5f);
            }
            else
            {
                // Destroy enemy instantly if no Spine animation is found
                Destroy(collision.gameObject);
            }

            Destroy(gameObject); // destroy fireball
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
