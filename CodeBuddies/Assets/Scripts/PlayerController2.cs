using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    public bool IsFinished { get; set; } = false;
    private Queue<string> _commandQueue1;
    private const float TileSize = 1f;
    private bool _isExecutingCommands;
    
    [SerializeField, Tooltip("Seconds between player 1 commands")]
    private float delayTime = 0.5f;
    [SerializeField, Tooltip("Starting positon: world space")]
    public Vector3 startingPosition;

    // UI elements
    public GameObject queuePanel;
    public Image upImage;
    public Image downImage;
    public Image leftImage;
    public Image rightImage;

    private Queue<Image> commandImagesQueue;

    void Start()
    {
        _commandQueue1 = new Queue<string>();
        commandImagesQueue = new Queue<Image>();
    }

    public void AddUpCommand()
    {
        _commandQueue1.Enqueue("Up");
        AddImageToQueue(upImage);
    }

    public void AddDownCommand()
    {
        _commandQueue1.Enqueue("Down");
        AddImageToQueue(downImage);
    }

    public void AddLeftCommand()
    {
        _commandQueue1.Enqueue("Left");
        AddImageToQueue(leftImage);
    }

    public void AddRightCommand()
    {
        _commandQueue1.Enqueue("Right");
        AddImageToQueue(rightImage);
    }

    private void AddImageToQueue(Image directionImage)
    {
        Image newDirectionImage = Instantiate(directionImage);

        newDirectionImage.transform.SetParent(queuePanel.transform, false);

        commandImagesQueue.Enqueue(newDirectionImage);
    }

    public void ExecuteCommands()
    {
        if (!_isExecutingCommands)
        {
            StartCoroutine(ExecuteCommandsWithDelay());
        }
    }

    private IEnumerator ExecuteCommandsWithDelay()
    {
        _isExecutingCommands = true;

        while (_commandQueue1.Count > 0)
        {
            string command = _commandQueue1.Dequeue();
            HandleCommand(command);

            if (commandImagesQueue.Count > 0)
            {
                Destroy(commandImagesQueue.Dequeue().gameObject);
            }

            yield return new WaitForSeconds(delayTime);
        }

        _isExecutingCommands = false;
        IsFinished = true;
    }

    private void HandleCommand(string command)
    {
        Vector3 moveAmount = Vector3.zero;

        switch (command)
        {
            case "Up":
                moveAmount = Vector3.up * TileSize;
                break;
            case "Down":
                moveAmount = Vector3.down * TileSize;
                break;
            case "Left":
                moveAmount = Vector3.left * TileSize;
                break;
            case "Right":
                moveAmount = Vector3.right * TileSize;
                break;
        }

        transform.Translate(moveAmount);
    }
    
    public void ClearCommands()
    {
        _commandQueue1.Clear();
        commandImagesQueue.Clear();
        IsFinished = false;
    }
}
