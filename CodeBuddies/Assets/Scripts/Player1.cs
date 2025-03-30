using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float pixelsPerMove = 100f; // Movement step size in pixels
    public float pixelsPerUnit = 100f; // Pixels per unit (PPU) from Unity settings
    private float moveStep; // Movement step in Unity units
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity for top-down movement
        moveStep = pixelsPerMove / pixelsPerUnit; // Convert pixels to Unity units
    }

    void Update()
    {
        Vector2 moveDirection = Vector2.zero;

        // Use GetKeyDown to detect a single key press
        if (Input.GetKeyDown(KeyCode.A)) moveDirection = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) moveDirection = Vector2.right;
        if (Input.GetKeyDown(KeyCode.W)) moveDirection = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) moveDirection = Vector2.down;

        // Move the player by one step
        transform.position += (Vector3)(moveDirection * moveStep);
    }
}
