using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public PlayerController1 player1;
    public PlayerController2 player2;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckForLoss()
    {
        if (player1.IsFinished && player2.IsFinished)
        {
            ResetPlayers();
        }
    }

    private void ResetPlayers()
    {
        Debug.Log("Both players lost! Resetting positions...");
        
        player1.transform.position = player1.startingPosition;
        player2.transform.position = player2.startingPosition;

        player1.IsFinished = false;
        player2.IsFinished = false;

        player1.ClearCommands();
        player2.ClearCommands();
    }
}