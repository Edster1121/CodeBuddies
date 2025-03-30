using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    [SerializeField, Tooltip("Starting position: world space")]
    public Vector3 startingPosition;

    // UI-related elements
    public GameObject queuePanel;

    [SerializeField, Tooltip("max images per row")]
    public int maxPerRow = 10;
    
    [SerializeField] private float imageScale = 0.5f;
    [SerializeField] private float spacingX = 20f;  // Adjust for proper horizontal spacing
    [SerializeField] private float spacingY = 20f;  // Adjust for vertical spacing
    [SerializeField] private float startX = 50f;  // Starting X position for the first image
    [SerializeField] private float startY = -50f; // Starting Y position for the first image


    public Image upImage;
    public Image downImage;
    public Image leftImage;
    public Image rightImage;

    private Queue<Image> commandImagesQueue;

    private void Start()
    {
        _commandQueue1 = new Queue<string>();
        commandImagesQueue = new Queue<Image>();
    }

    public void AddUpCommand()
    {
        if (commandImagesQueue.Count < 2 * maxPerRow)  // Limit to 2 rows
        {
            _commandQueue1.Enqueue("Up");
            AddImageToQueue(upImage);
        }
    }

    public void AddDownCommand()
    {
        if (commandImagesQueue.Count < 2 * maxPerRow)  // Limit to 2 rows
        {
            _commandQueue1.Enqueue("Down");
            AddImageToQueue(downImage);
        }
    }

    public void AddLeftCommand()
    {
        if (commandImagesQueue.Count < 2 * maxPerRow)  // Limit to 2 rows
        {
            _commandQueue1.Enqueue("Left");
            AddImageToQueue(leftImage);
        }
    }

    public void AddRightCommand()
    {
        if (commandImagesQueue.Count < 2 * maxPerRow)  // Limit to 2 rows
        {
            _commandQueue1.Enqueue("Right");
            AddImageToQueue(rightImage);
        }
    }

    private void AddImageToQueue(Image directionImage)
    {
        
        if (commandImagesQueue.Count >= 2 * maxPerRow)
        {
            Debug.Log("Maximum image limit reached (2 rows).");
            return;
        }
        
        Image newDirectionImage = Instantiate(directionImage);
        newDirectionImage.transform.SetParent(queuePanel.transform, false);

        // shrink and add to queue
        newDirectionImage.transform.localScale = new Vector3(imageScale, imageScale, 1f);
        commandImagesQueue.Enqueue(newDirectionImage);
        UpdateImagePositions();
    }

    private void UpdateImagePositions()
    {
        for (int i = 0; i < commandImagesQueue.Count; i++)
        {
            int row = i / maxPerRow;  // Moves to the next row after maxPerRow images
            int col = i % maxPerRow;  // Determines position in the current row

            RectTransform rect = commandImagesQueue.ElementAt(i).GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(startX + col * spacingX, startY - (row * spacingY));
        }
    }

    // Execute the commands in the queue
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

            // Optionally remove the first image from the UI after it's executed
            if (commandImagesQueue.Count > 0)
            {
                Destroy(commandImagesQueue.Dequeue().gameObject);  // Destroy the image from the UI
            }

            yield return new WaitForSeconds(delayTime);
        }

        _isExecutingCommands = false;
        IsFinished = true;
        GameManager.Instance.CheckForWin();
        GameManager.Instance.CheckForLoss();
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
