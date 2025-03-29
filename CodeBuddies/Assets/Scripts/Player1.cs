using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    public float pixelsPerMove = 16f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = 0; // disable gravity
    }

    // Update is called once per frame
    void Update()
    {
        // Only use WASD for Player 1
        if (Input.GetKey(KeyCode.A)) rb.transform.Translate(Vector2.left * pixelsPerMove);
        if (Input.GetKey(KeyCode.D)) rb.transform.Translate(Vector2.right * pixelsPerMove);
        if (Input.GetKey(KeyCode.W)) rb.transform.Translate(Vector2.up * pixelsPerMove);
        if (Input.GetKey(KeyCode.S)) rb.transform.Translate(Vector2.down * pixelsPerMove);
    }
}
