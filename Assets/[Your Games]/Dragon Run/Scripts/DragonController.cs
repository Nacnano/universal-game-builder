using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class DragonController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Flight Settings")]
    public float flyUpY = 3f;
    public float walkDownY = -3f;
    public float transitionDuration = 0.3f;

    [Header("Fire Breath Settings")]
    public GameObject fireBreathPrefab;
    public Transform fireSpawnPoint;
    public float fireCooldown = 1f;

    [Header("Visuals (Optional)")]
    public Transform dragonVisual;
    public Vector3 flyScale = new Vector3(1.1f, 1.1f, 1f);
    public Vector3 walkScale = new Vector3(1f, 1f, 1f);

    private float lastFireTime = 0f;
    private bool isFlying = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, walkDownY, transform.position.z);
    }

    private void Update()
    {
        if (DragonGameManager.Instance == null || !DragonGameManager.Instance.isGameActive)
            return;

        HandleFlightInput();
        HandleFireInput();
    }

    private void HandleFlightInput()
    {
        // Press or hold button to fly up, release to fly down (default mapped to Space or MoveUp)
        bool flyInput = false;

        if (GameInputManager.Instance != null)
        {
            flyInput = GameInputManager.Instance.IsActionPressed(InputActionType.MoveUp) || 
                       GameInputManager.Instance.IsActionPressed(InputActionType.LiftLeftPlatform) ||
                       Input.GetKey(KeyCode.Space);
        }
        else
        {
            flyInput = Input.GetKey(KeyCode.Space);
        }

        if (flyInput && !isFlying)
        {
            isFlying = true;
            transform.DOMoveY(flyUpY, transitionDuration).SetEase(Ease.OutSine);
            
            if (dragonVisual != null)
                dragonVisual.DOScale(flyScale, transitionDuration);
        }
        else if (!flyInput && isFlying)
        {
            isFlying = false;
            transform.DOMoveY(walkDownY, transitionDuration).SetEase(Ease.OutSine);
            
            if (dragonVisual != null)
                dragonVisual.DOScale(walkScale, transitionDuration);
        }
    }

    private void HandleFireInput()
    {
        // Require button to be pressed down each time (not held) to shoot fire
        bool fireInput = false;

        if (GameInputManager.Instance != null)
        {
            fireInput = GameInputManager.Instance.IsActionDown(InputActionType.MoveRight) || 
                        GameInputManager.Instance.IsActionDown(InputActionType.LiftRightPlatform) ||
                        Input.GetKeyDown(KeyCode.LeftControl); // Fallback key
        }
        else
        {
            fireInput = Input.GetKeyDown(KeyCode.LeftControl);
        }

        // Fire breath logic
        if (fireInput && Time.time >= lastFireTime + fireCooldown)
        {
            lastFireTime = Time.time;
            ShootFire();
        }
    }

    private void ShootFire()
    {
        if (fireBreathPrefab != null && fireSpawnPoint != null)
        {
            Instantiate(fireBreathPrefab, fireSpawnPoint.position, fireSpawnPoint.rotation);
        }

        // Visual feedback for firing
        if (dragonVisual != null)
        {
            dragonVisual.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.2f, 10, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DragonGameManager.Instance == null || !DragonGameManager.Instance.isGameActive)
            return;

        // Take damage from obstacles or monsters
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            DragonGameManager.Instance.TakeDamage(1);

            // Give a momentary immune flash or push back effect
            if (dragonVisual != null)
            {
                dragonVisual.GetComponent<SpriteRenderer>()?.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
            }
            
            // Destroy the obstacle to prevent multiple hits
            Destroy(collision.gameObject);
        }
    }
}
