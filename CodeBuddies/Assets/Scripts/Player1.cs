using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    private Queue<string> commandQueue;
    private const float tileSize = 1f;
    private bool isExecutingCommands = false;

    [SerializeField, Tooltip("Seconds between player 1 commands")]
    private float delayTime = 0.5f;

    [SerializeField, Tooltip("Layer for obstacles")]
    private LayerMask obstacleLayer;

    void Start()
    {
        commandQueue = new Queue<string>();
    }

    public void AddUpCommand() => commandQueue.Enqueue("Up");
    public void AddDownCommand() => commandQueue.Enqueue("Down");
    public void AddLeftCommand() => commandQueue.Enqueue("Left");
    public void AddRightCommand() => commandQueue.Enqueue("Right");

    public void ExecuteCommands()
    {
        if (!isExecutingCommands)
        {
            StartCoroutine(ExecuteCommandsWithDelay());
        }
    }

    private IEnumerator ExecuteCommandsWithDelay()
    {
        isExecutingCommands = true;

        while (commandQueue.Count > 0)
        {
            string command = commandQueue.Peek(); // Peek without removing
            if (CanMove(command)) 
            {
                commandQueue.Dequeue(); // Remove command only if movement is valid
                HandleCommand(command);
                yield return new WaitForSeconds(delayTime);
            }
            else
            {
                Debug.Log("Blocked by boulder! Stopping movement.");
                break; // Stop execution if a boulder is ahead
            }
        }

        isExecutingCommands = false;
    }

    private void HandleCommand(string command)
    {
        Vector3 moveAmount = Vector3.zero;

        switch (command)
        {
            case "Up":
                moveAmount = Vector3.up * tileSize;
                break;
            case "Down":
                moveAmount = Vector3.down * tileSize;
                break;
            case "Left":
                moveAmount = Vector3.left * tileSize;
                break;
            case "Right":
                moveAmount = Vector3.right * tileSize;
                break;
        }

        transform.position += moveAmount;
    }

    private bool CanMove(string command)
    {
        Vector3 nextPosition = transform.position;

        switch (command)
        {
            case "Up":
                nextPosition += Vector3.up * tileSize;
                break;
            case "Down":
                nextPosition += Vector3.down * tileSize;
                break;
            case "Left":
                nextPosition += Vector3.left * tileSize;
                break;
            case "Right":
                nextPosition += Vector3.right * tileSize;
                break;
        }

        // Check for colliders at the next position
        Collider2D hit = Physics2D.OverlapCircle(nextPosition, 0.1f, obstacleLayer);

        return hit == null; // If no collider is found, movement is allowed
    }

//temp
    private float moveSpeed = 2f; // Speed in units per second

    void Update()
    {
        // Move the object continuously to the right
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

}

    
