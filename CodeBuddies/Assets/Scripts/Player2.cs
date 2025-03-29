using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{

    public float pixelsPerMove = 16f;
    public float pixelsPerUnit = 100f;
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.gravityScale = 0; // disable gravity
        moveSpeed = (pixelsPerMove / pixelsPerUnit);
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;    

        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;
        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;

        // Normalize movement to prevent fast diagonal travel
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // Apply movement using Rigidbody2D
        rb.velocity = moveDirection * moveSpeed / Time.fixedDeltaTime;
    }
}
