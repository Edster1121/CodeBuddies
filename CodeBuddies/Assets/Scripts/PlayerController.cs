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
            HandleCommand(command);
            yield return new WaitForSeconds(delayTime);
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

        transform.Translate(moveAmount);
    }
}