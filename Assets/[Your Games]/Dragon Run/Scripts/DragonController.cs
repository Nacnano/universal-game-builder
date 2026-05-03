using UnityEngine;
using DG.Tweening;
using Spine.Unity;

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

    [Header("Visuals (Spine 2D)")]
    public Transform dragonVisual;
    public SkeletonAnimation dragonSkeleton;
    [SpineAnimation(dataField: "dragonSkeleton")] public string walkAnimationName = "Walk";
    [SpineAnimation(dataField: "dragonSkeleton")] public string flyAnimationName = "Walk"; // Or use Attack/Idle if no Fly exists
    [SpineAnimation(dataField: "dragonSkeleton")] public string attackAnimationName = "Attack";
    [SpineAnimation(dataField: "dragonSkeleton")] public string deadAnimationName = "Dead";
    
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
        if (dragonSkeleton != null && !string.IsNullOrEmpty(walkAnimationName))
        {
            dragonSkeleton.AnimationState.SetAnimation(0, walkAnimationName, true);
        }
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

            if (dragonSkeleton != null && !string.IsNullOrEmpty(flyAnimationName))
                dragonSkeleton.AnimationState.SetAnimation(0, flyAnimationName, true);
        }
        else if (!flyInput && isFlying)
        {
            isFlying = false;
            transform.DOMoveY(walkDownY, transitionDuration).SetEase(Ease.OutSine);
            
            if (dragonVisual != null)
                dragonVisual.DOScale(walkScale, transitionDuration);

            if (dragonSkeleton != null && !string.IsNullOrEmpty(walkAnimationName))
                dragonSkeleton.AnimationState.SetAnimation(0, walkAnimationName, true);
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

        // Spine attack animation layered on track 1 so it doesn't interrupt the leg/wing cycle completely
        if (dragonSkeleton != null && !string.IsNullOrEmpty(attackAnimationName))
        {
            dragonSkeleton.AnimationState.SetAnimation(1, attackAnimationName, false);
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

            // Play dead animation if dead, otherwise hit flash
            if (dragonSkeleton != null && !string.IsNullOrEmpty(deadAnimationName) && DragonGameManager.Instance.currentHealth <= 0)
            {
                dragonSkeleton.AnimationState.SetAnimation(2, deadAnimationName, false);
            }
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
