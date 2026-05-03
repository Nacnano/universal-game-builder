using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AutoParallax : MonoBehaviour
{
    [Tooltip("How fast this layer moves relative to the game speed. Examples: Ground = 1.0, Background = 0.2")]
    public float speedMultiplier = 1f;

    private float rawSpriteWidth;
    private float worldSpriteWidth;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        
        rawSpriteWidth = sr.sprite.bounds.size.x;
        worldSpriteWidth = sr.bounds.size.x;

        // Create the clone child to loop alongside it automatically
        GameObject clone = new GameObject(gameObject.name + "_Clone");
        clone.transform.SetParent(transform); 
        // localPosition uses raw width because it gets multiplied by the parent's scale
        clone.transform.localPosition = new Vector3(rawSpriteWidth, 0f, 0f); 
        clone.transform.localScale = Vector3.one;

        // Copy rendering settings to the clone
        SpriteRenderer cloneSr = clone.AddComponent<SpriteRenderer>();
        cloneSr.sprite = sr.sprite;
        cloneSr.color = sr.color;
        cloneSr.sortingLayerID = sr.sortingLayerID;
        cloneSr.sortingLayerName = sr.sortingLayerName;
        cloneSr.sortingOrder = sr.sortingOrder;
    }

    private void Update()
    {
        if (DragonGameManager.Instance == null || !DragonGameManager.Instance.isGameActive)
            return;

        // Move to the left based on global speed and the parallax multiplier
        float moveDistance = DragonGameManager.Instance.CurrentMoveSpeed * speedMultiplier * Time.deltaTime;
        transform.position += Vector3.left * moveDistance;

        // If the object has moved completely off by exactly one full width, snap it back!
        if (transform.position.x <= startPosition.x - worldSpriteWidth)
        {
            float overshoot = (startPosition.x - worldSpriteWidth) - transform.position.x;
            transform.position = new Vector3(startPosition.x - overshoot, transform.position.y, transform.position.z);
        }
    }
}
