using UnityEngine;

public class ResetTile : MonoBehaviour
{
    private Vector3 startingPosition; // Store the starting position

    void Start()
    {
        startingPosition = transform.position; // Save the player's start position
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ResetTile")) // Check if the player touches the reset tile
        {
            Debug.Log("Player landed on reset tile! Returning to start.");
            transform.position = startingPosition; // Move player back to start
        }
    }
}
