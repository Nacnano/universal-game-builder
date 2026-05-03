using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (DragonGameManager.Instance == null || !DragonGameManager.Instance.isGameActive)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Move to the left based on global speed
        rb.velocity = Vector2.left * DragonGameManager.Instance.CurrentMoveSpeed;

        // Destroy when completely out of screen on the left (-15 units)
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
