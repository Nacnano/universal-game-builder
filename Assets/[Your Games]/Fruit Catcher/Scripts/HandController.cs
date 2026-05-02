using UnityEngine;
using DG.Tweening;

public class HandController : MonoBehaviour
{
    [Header("Hand Settings")]
    public float activeDuration = 0.5f;
    public Collider2D catchCollider;
    
    [Header("Visuals")]
    public Transform handVisual;
    public Vector3 idleScale = Vector3.one;
    public Vector3 activeScale = new Vector3(1.5f, 1.5f, 1f);
    public Color idleColor = Color.white;
    public Color activeColor = Color.green;
    private SpriteRenderer handRenderer;

    private bool isActive = false;
    private float activeTimer = 0f;

    private void Start()
    {
        if (catchCollider != null)
            catchCollider.enabled = false;
            
        if (handVisual != null)
        {
            handRenderer = handVisual.GetComponent<SpriteRenderer>();
        }
        
        SetIdleState();
    }

    private void Update()
    {
        // Handle input - grab it from GameInputManager (using LiftLeftPlatform or Spacebar as action button)
        if (GameInputManager.Instance != null && !isActive)
        {
            if (GameInputManager.Instance.IsActionDown(InputActionType.LiftLeftPlatform) || 
                GameInputManager.Instance.IsActionDown(InputActionType.MoveUp) ||
                Input.GetKeyDown(KeyCode.Space))
            {
                ActivateHand();
            }
        }

        // Handle Active Timer
        if (isActive)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0f)
            {
                SetIdleState();
            }
        }
    }

    private void ActivateHand()
    {
        isActive = true;
        activeTimer = activeDuration;
        
        if (catchCollider != null)
            catchCollider.enabled = true;

        if (handVisual != null)
        {
            handVisual.DOKill();
            // Punch visual to show activation
            handVisual.DOScale(activeScale, 0.1f).OnComplete(() => {
                handVisual.DOScale(idleScale, activeDuration - 0.1f);
            });
            
            if (handRenderer != null)
            {
                handRenderer.DOColor(activeColor, 0.1f).OnComplete(() => {
                    handRenderer.DOColor(idleColor, activeDuration - 0.1f);
                });
            }
        }
    }

    private void SetIdleState()
    {
        isActive = false;
        
        if (catchCollider != null)
            catchCollider.enabled = false;

        if (handVisual != null)
        {
            handVisual.DOKill();
            handVisual.localScale = idleScale;
            if (handRenderer != null)
            {
                handRenderer.color = idleColor;
            }
        }
    }
}
