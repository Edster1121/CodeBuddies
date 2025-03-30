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
        string command = commandQueue.Dequeue();

        // if (!CanMove(command)) 
        // {
        //     Debug.Log("Movement blocked! Stopping execution.");
        //     isExecutingCommands = false;  // Reset flag so new commands can execute later
        //     commandQueue.Clear();
        //     yield break;  // Exit coroutine safely
        // }

        if (CanMove(command)) 
        {
            HandleCommand(command);
        }
        
        yield return new WaitForSeconds(delayTime);
    }

    isExecutingCommands = false;  // Ensure coroutine can be restarted when new commands are added
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

        transform.Translate(moveAmount);
    }

    private bool CanMove(string command)
    {
        Vector3 direction = Vector3.zero;

        switch (command)
        {
            case "Up":
                direction = Vector3.up;
                break;
            case "Down":
                direction = Vector3.down;
                break;
            case "Left":
                direction = Vector3.left;
                break;
            case "Right":
                direction = Vector3.right;
                break;
        }

        float checkDistance = tileSize * 0.9f; // Slightly less than tile size to avoid precision errors
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, checkDistance, obstacleLayer);

        return hit.collider == null; // If we hit an obstacle, we can't move
    }

}